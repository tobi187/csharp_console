using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console
{
    // add additional libraries if required

    public class Challenges
    {
        public bool ValidatePassword(string password)
        {
            if (password.Length < 6 || password.Length > 24) return false;
            return true;

        }
    }

    public class Allergies
    {
        // do not alter this enum
        [Flags]
        public enum Allergen
        {
            Eggs = 1,
            Peanuts = 2,
            Shellfish = 4,
            Strawberries = 8,
            Tomatoes = 16,
            Chocolate = 32,
            Pollen = 64,
            Cats = 128
        }

        // write your code below this line

        // constructors 
        public Allergies(string name)
        {
            Name = name;
            Score = 0;
        }

        public Allergies(string name, int score)
        {
            Name = name;
            Score = score;
        }
        public Allergies(string name, string allergies)
        {
            Name = name;
            var allAllergies = allergies.Split();
            foreach (var a in allAllergies)
            {
                var e = ParseEnum(a);
                Score += (int)e;
                MyAllergies.Add(e);
            }
        }
        // properties
        public readonly string Name;
        public readonly int Score;
        private List<Allergen> MyAllergies = new List<Allergen>();
        // methods

        public void decoupleAllergies(List<Allergen> allergens, int score)
        {
            if (score == 0)
            {
                MyAllergies = allergens;
                return;
            }

        }

        public override string ToString()
        {
            // add code here to return string representation of this instance
            return base.ToString();
        }

        public Allergen ParseEnum(string name)
        {
            return Enum.Parse<Allergen>(name);
        }
    }
}
