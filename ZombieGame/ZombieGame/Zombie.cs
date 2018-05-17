using System;

namespace ZombieGame
{
    public class Zombie
    {
        string Name { get; set; }
        uint Dna { get; set; }

        public Zombie()
        {

        }

        public Zombie(string name, uint dna)
        {
            this.Name = name;
            this.Dna = dna;
        }
    }
}
