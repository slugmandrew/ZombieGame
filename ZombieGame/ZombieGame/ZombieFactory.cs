using Stratis.SmartContracts;
using System;
using System.Collections;
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

    }

}