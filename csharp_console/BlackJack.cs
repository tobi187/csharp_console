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
         
        internal BlackJack(List<string> playerNames, string dealerName)
        {
            Players = playerNames.Select(x => new Player(x, this)).ToList();
            Dealer = new Dealer(dealerName, this);
        }

        public void PlayRound()
        {
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
            var noLooser = Players.Where(x => x.State != PlayerState.IsOver21);
            var dealerSum = Dealer.State == PlayerState.IsOver21 ? -1 : Dealer.getSum();
            var player = noLooser.MaxBy(x => x.getSum());
            
            if (player == null && dealerSum < -1)
                Console.WriteLine("Ihr seid alle so schlecht");

            if (player == null || Dealer.getSum() > player.getSum())
            {
                Dealer.MeWon();
                return;
            }
            
            var winners = noLooser.Where(x => x.getSum() == player.getSum()).ToList();
            winners.ForEach(x => x.MeWon());
        }

    }

    internal class Player
    {
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
            Console.Write($"Do you want to draw {Name} ? (n for no)");

            if (Console.ReadLine() == "n")
            {
                State = PlayerState.StoppedDrawing;
                return;
            }
            Draw();
        }

        private void Draw()
        {
            HandCards.Add(new Card());

            CheckAce();

            CheckCards();
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
            return 
                HandCards.Sum(x => (int)x.Value);
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
            Console.WriteLine($"{Name} hat gewonnen");
            Wins++;
        }

        public void NewRound()
        {
            HandCards.Clear();
            HandCards.Add(new Card(isOpen: true));
            HandCards.Add(new Card());
            State = PlayerState.IsDrawing;
        }
    }

    internal class Dealer : Player
    {
        internal Dealer(string name, BlackJack game) : base(name, game) {}

        public override void DrawCard()
        {
            var sum = getSum();

            if (sum < 17)
                DrawCard();
            else
                return;
        }
    }

    internal class Card
    {
        public CardType Type { get; }
        public CardValue Value { get; private set; }
        public bool IsOpen;
        private Random _random = new Random();

        internal Card(bool isOpen = false)
        {
            var cardTypes = Enum.GetValues<CardType>();
            var cardValues = Enum.GetValues<CardValue>();
            Type = (CardType)cardTypes.GetValue(_random.Next(cardTypes.Length));
            Value = (CardValue)cardValues.GetValue(_random.Next(cardTypes.Length));
            IsOpen = isOpen;
        }

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
            return $"{Type} {(int)Value}";
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
        AceSmall = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10,
        Ace = 11
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
