using Stratis.SmartContracts;
using System;
using System.Text;

public class ZombieFactory : SmartContract
{
    public uint dnaDigits;
    public long dnaModulus;

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
        this.dnaModulus = Pow(10, dnaDigits);
    }

    long Pow(long x, uint pow)
    {
        long ret = 1;
        while (pow != 0)
        {
            if ((pow & 1) == 1)
                ret *= x;
            x *= x;
            pow >>= 1;
        }
        return ret;
    }

    public void CreateRandomZombie(string name)
    {
        uint dna = GenerateRandomDna(name);
        CreateZombie(name, dna);
    }

    private void CreateZombie(string name, uint dna)
    {
        PersistentState.GetList<Zombie>("zombies").Add(new Zombie(name, dna));
    }

    private uint GenerateRandomDna(string name)
    {
        byte[] rand = Keccak256(Encoding.ASCII.GetBytes(name));

        return BitConverter.ToUInt32(rand, 0);
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
