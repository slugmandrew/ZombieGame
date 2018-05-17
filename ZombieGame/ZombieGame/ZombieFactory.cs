using Stratis.SmartContracts;
using System;
using System.Text;

public class ZombieFactory : SmartContract
{
    public uint dnaDigits;
    public uint dnaModulus;


    public ISmartContractList<Zombie> Zombies
    {
        get
        {
            return PersistentState.GetList<Zombie>("zombies");
        }
    }


    public ZombieFactory(ISmartContractState smartContractState) : base(smartContractState)
    {
        this.dnaDigits = 16;
        this.dnaModulus = UIntPow(10, dnaDigits);
    }

    private uint UIntPow(uint x, uint pow)
    {
        uint ret = 1;
        while (pow != 0)
        {
            if ((pow & 1) == 1)
                ret *= x;
            x *= x;
            pow >>= 1;
        }
        return ret;
    }


    private void CreateZombie(string name, uint dna)
    {
        PersistentState.GetList<Zombie>("zombies").Add(new Zombie(name, dna));
    }

    private string GenerateRandomDna(string name)
    {
        byte[] rand = Keccak256(Encoding.ASCII.GetBytes(name));

        return Encoding.ASCII.GetString(rand);
    }


    public struct Zombie
    {
        string Name { get; set; }
        uint Dna { get; set; }

        public Zombie(string name, uint dna)
        {
            this.Name = name;
            this.Dna = dna;
        }
    }

}
