using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console
{
    internal class BlackJack // Game
    {
        public List<Player> Players { get; private set; }
        public Player Dealer { get; private set; }
        public Deck Deck { get; private set; }

        internal BlackJack(List<string> playerNames, string dealerName)
        {
            Players = playerNames.Select(x => new Player(x, this)).ToList();
            Dealer = new Dealer(dealerName, this);
            Deck = new Deck();
        }

        public List<Player> PlayersWithDealer()
        {
            var copy = Players.ToList();
            copy.Add(Dealer);
            return copy;
        }

        public void PlayRound()
        {
            var deck = new Deck();

            Dealer.NewRound();
            Players.ForEach(x => x.NewRound());

            while (!GameIsOver())
            {
                Players.ForEach(x => x.DrawCard());
                Dealer.DrawCard();
            }

            DetermineWinner();

            Console.WriteLine("Wanna Play again ? (y/n)");
            if (Console.ReadLine() == "y")
                PlayRound();
        }

        public bool GameIsOver()
        {
            var activePlayers = Players.Count(x => x.State == PlayerState.IsDrawing);
            var dealerPlaying = Dealer.State == PlayerState.IsDrawing ? 1 : 0;
            if (activePlayers + dealerPlaying > 1)
                return false;
            else
                return true;
        }

        public void DetermineWinner()
        {
            var playerWhichCanWin = PlayersWithDealer().Where(x => x.State != PlayerState.IsOver21);
            var highScore = playerWhichCanWin.MaxBy(x => x.getSum());
            if (highScore == null)
            {
                ShowWinnerBoard();
                return;
            }

            var winner = playerWhichCanWin.Where(x => x.getSum() == highScore.getSum());

            if (winner.Count() > 1)
                ShowWinnerBoard(winner.ToList());
            else
                ShowWinnerBoard(winner.Single());

        }
        // or insert playername instead of reference
        public void ShowBoard(Player currentPlayer)
        {
            foreach (var player in PlayersWithDealer())
            {
                if (player == currentPlayer) continue;
                var openCard = player.HandCards.Single(x => x.IsOpen);
                if (player.State == PlayerState.IsDrawing || player.State == PlayerState.StoppedDrawing)
                    Console.WriteLine($"{player.Name} has a {openCard} and {player.HandCards.Count - 1} Cards reversed");
                else if (player.State == PlayerState.IsOver21)
                    Console.WriteLine($"{player.Name} has Gone over 21 with {player.ShowCards()}");
            }
            Console.WriteLine($"You have {string.Join(", ", currentPlayer.HandCards)}. Your Total is {currentPlayer.getSum()}");

        }

        public void ShowWinnerBoard()
        {
            Console.WriteLine("EndBoard");
            PlayersWithDealer().ForEach(x => Console.WriteLine($"{x.Name} has finished with {x.ShowCards()}"));
            Console.WriteLine("\nYou are all very not good");
        }
        public void ShowWinnerBoard(Player winner)
        {
            Console.WriteLine("EndBoard");
            foreach (var x in PlayersWithDealer().Where(x => x != winner))
                Console.WriteLine($"{x.Name} has finished with {x.ShowCards()}");
            Console.WriteLine($"\nCongratulations {winner.Name}");
            Console.WriteLine($"You've won with a hand of {winner.ShowCards()}");

            winner.MeWon();
        }

        public void ShowWinnerBoard(List<Player> winners)
        {
            Console.WriteLine("Endboard");
            foreach (var x in PlayersWithDealer().Where(x => !winners.Contains(x)))
            {
                Console.WriteLine($"{x.Name} has finished with {x.ShowCards()}");
            }
            Console.WriteLine($"\n{string.Join(", ", winners.Select(x => x.Name))} have tied. Congratulations to y'all");
            winners.ForEach(x => Console.WriteLine($"{x.Name} was a winner with a hand of {x.ShowCards()}"));
            winners.ForEach(x => x.MeWon());
        }
    }

    internal class Player
    {
        // 2 Karte bei allen sichtbar ausser Dealer
        // bei aufdecken natural blackjack gewinnt 
        public string Name { get; }
        public List<Card> HandCards { get; set; } // = new List<Card>();
        public int Wins { get; private set; } = 0;
        public PlayerState State { get; private set; }
        private BlackJack Game;

        internal Player(string name, BlackJack game)
        {
            Name = name;
            HandCards = new List<Card>();
            Game = game;
        }

        public virtual void DrawCard()
        {
            //if (Game.GameIsOver()) return;
            Game.ShowBoard(this);
            Console.Write($"Do you want to draw {Name} ? (n for no) ");

            if (Console.ReadLine() == "n")
            {
                State = PlayerState.StoppedDrawing;
                return;
            }
            Draw();
        }

        private protected void Draw()
        {
            HandCards.Add(Game.Deck.GetCard());

            CheckAce();

            CheckCards();
        }

        public string ShowCards()
        {

            return string.Join(",", HandCards);

        }

        private void CheckCards()
        {
            var cSum = getSum();
            if (cSum > 21)
            {
                State = PlayerState.IsOver21;
            }
            if (cSum == 21)
            {
                State = PlayerState.Has21;
            }
        }

        public int getSum()
        {
            var cardValues =
                new Dictionary<CardValue, int>() {
                    { CardValue.Two, 2 },
                    { CardValue.Three, 3 },
                    { CardValue.Four, 4 },
                    { CardValue.Five, 5 },
                    { CardValue.Six, 6 },
                    { CardValue.Seven, 7 },
                    { CardValue.Eight, 8 },
                    { CardValue.Nine, 9 },
                    { CardValue.Ten, 10 },
                    { CardValue.Jack, 10 },
                    { CardValue.Queen, 10 },
                    { CardValue.King, 10 },
                    { CardValue.Ace, 11 }
                };

            return
                HandCards.Sum(x => x.GetValue());
        }

        private void CheckAce()
        {
            var ace = HandCards.FindIndex(x => x.Value == CardValue.Ace);
            if (ace == -1)
                return;

            if (getSum() < 22)
                return;

            HandCards[ace].changeAceValue();
        }

        public void MeWon()
        {
            //Game.ShowBoard(this);
            Wins++;
        }

        public void NewRound()
        {
            HandCards.Clear();
            HandCards.AddRange(Game.Deck.InitalCards());
            State = PlayerState.IsDrawing;
        }
    }

    internal class Dealer : Player
    {
        internal Dealer(string name, BlackJack game) : base(name, game) { }

        public override void DrawCard()
        {
            var sum = getSum();

            if (sum < 17)
                Draw();
        }
    }

    internal class Deck
    {
        private List<Card> Cards;
        internal Deck()
        {
            Cards =
                (from t in Enum.GetValues<CardType>()
                 from v in Enum.GetValues<CardValue>()
                 where v != CardValue.AceSmall
                 select new Card(t, v))
                 .ToList();

            Cards = Cards.OrderBy(_ => new Guid()).ToList();
        }

        public Card GetCard()
        {
            var card = Cards.First();
            Cards.Remove(card);
            return card;
        }

        public List<Card> InitalCards()
        {
            var cards = Cards.Take(2).ToList();
            Cards.RemoveRange(0, 2);
            cards[0].IsOpen = true;
            return cards;
        }
    }

    internal class Card
    {
        public CardType Type { get; }
        public CardValue Value { get; private set; }
        public bool IsOpen { get; set; }
        private Dictionary<CardValue, int> cardParser = new Dictionary<CardValue, int>() {
                    { CardValue.Two, 2 },
                    { CardValue.Three, 3 },
                    { CardValue.Four, 4 },
                    { CardValue.Five, 5 },
                    { CardValue.Six, 6 },
                    { CardValue.Seven, 7 },
                    { CardValue.Eight, 8 },
                    { CardValue.Nine, 9 },
                    { CardValue.Ten, 10 },
                    { CardValue.Jack, 10 },
                    { CardValue.Queen, 10 },
                    { CardValue.King, 10 },
                    { CardValue.Ace, 11 }
                };

        internal Card(CardType type, CardValue value, bool isOpen = false)
        {
            Type = type;
            Value = value;
            IsOpen = isOpen;
        }

        public void changeAceValue()
        {
            Value = CardValue.AceSmall;
        }

        public override string ToString()
        {
            return $"{Type} {Value}";
        }

        public int GetValue()
        {
            return cardParser[Value];
        }
    }

    enum CardType
    {
        Heart,
        Diamond,
        Spade,
        Club
    }

    enum CardValue
    {
        AceSmall,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    enum PlayerState
    {
        IsDrawing,
        StoppedDrawing,
        IsOver21,
        Has21,
        HasSurrendered,
    }
}
