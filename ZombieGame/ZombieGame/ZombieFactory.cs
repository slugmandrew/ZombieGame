using Stratis.SmartContracts;
using System;
using System.Collections.Generic;

namespace ZombieGame
{

    public class ZombieFactory : SmartContract
    {
        public uint dnaDigits = 16;

        public Double DnaModulus
        {
            get
            {
                return Math.Pow(10, dnaDigits);
            }
        }

        public List<Zombie> zombies = new List<Zombie>();


        public ZombieFactory(ISmartContractState smartContractState) : base(smartContractState)
        {

        }

        public void CreateZombie(string name, uint dna)
        {
            zombies.Add(new Zombie(name, dna));
        }

    }

}