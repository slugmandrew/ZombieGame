using Stratis.SmartContracts;
using System;

public class ZombieFactory : SmartContract
{
    public uint DnaDigits
    {
        get
        {
            return PersistentState.GetUInt32("DnaDigits");
        }
        set
        {
            PersistentState.SetUInt32("DnaDigits", value);
        }
    }

    public ulong DnaModulus
    {
        get
        {
            return PersistentState.GetUInt64("DnaModulus");
        }
        set
        {
            PersistentState.SetUInt64("DnaModulus", value);
        }
    }

    public Address Owner
    {
        get
        {
            return PersistentState.GetAddress("Owner");
        }
        set
        {
            PersistentState.SetAddress("Owner", value);
        }
    }

    public ulong EndBlock
    {
        get
        {
            return PersistentState.GetUInt64("EndBlock");
        }
        set
        {
            PersistentState.SetUInt64("EndBlock", value);
        }
    }

    public bool HasEnded
    {
        get
        {
            return PersistentState.GetBool("HasEnded");
        }
        set
        {
            PersistentState.SetBool("HasEnded", value);
        }
    }

    public ISmartContractList<Zombie> Zombies
    {
        get
        {
            return PersistentState.GetStructList<Zombie>("zombies");
        }
    }

    public ZombieFactory(ISmartContractState smartContractState, ulong durationBlocks) : base(smartContractState)
    {
        Owner = Message.Sender;
        EndBlock = Block.Number + durationBlocks;
        HasEnded = false;
        DnaDigits = 16;
        DnaModulus = Pow(10, DnaDigits);
    }

    public void CreateRandomZombie(string name)
    {
        ulong dna = GenerateRandomDna(name);
        CreateZombie(name, dna);
    }

    public void CreateZombie(string name, ulong dna)
    {
        Zombies.Add(new Zombie(name, dna));
    }

    private ulong GenerateRandomDna(string name)
    {
        return HashStringTo18DigitNumber(name) % DnaModulus;
    }

    public struct Zombie
    {
        string Name { get; set; }
        ulong Dna { get; set; }

        public Zombie(string name, ulong dna)
        {
            this.Name = name;
            this.Dna = dna;
        }
    }


    // ********** BEGIN My custom methods

    private ulong Pow(ulong x, uint pow)
    {
        ulong ret = 1;
        while (pow != 0)
        {
            if ((pow & 1) == 1)
                ret *= x;
            x *= x;
            pow >>= 1;
        }
        return ret;
    }

    // ********** END My custom methods



    // ********** BEGIN Hashing helper methods from https://github.com/StratisDevelopmentFoundation/Sample_Stratis_Smart_Contracts/blob/master/HashString.cs

    public ulong HashStringTo18DigitNumber(string s)
    {
        byte[] bytes = StringToByteArray(s);
        var firstHash = Keccak256(bytes);
        var hashedBytes = Keccak256(firstHash);
        var stringOfNumbers = byteArrayToNumberString(hashedBytes);
        return (ulong)Int64.Parse(stringOfNumbers.Substring(Math.Max(stringOfNumbers.Length - UInt64.MaxValue.ToString().Length, 0)));
    }

    public string HashStringToAlphaNumericString(string s)
    {
        byte[] bytes = StringToByteArray(s);
        var firstHash = Keccak256(bytes);
        var hashedBytes = Keccak256(firstHash);
        return byteArrayToAlphaNumericString(hashedBytes);
    }

    private byte[] StringToByteArray(string s)
    {
        var chars = s.ToCharArray();
        byte[] bytes = new byte[chars.Length];
        var loopcount = 0;
        foreach (var singleCharacter in chars)
        {
            bytes[loopcount] = (byte)chars[loopcount];
            loopcount++;
        }
        return bytes;
    }

    private string byteArrayToNumberString(byte[] bytes)
    {
        var outputString = "";
        foreach (var singleByte in bytes)
        {
            outputString = outputString + singleByte.ToString();
        }
        return outputString;
    }

    private string byteArrayToAlphaNumericString(byte[] bytes)
    {
        var base58 = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        var outputString = "";
        foreach (var singleByte in bytes)
        {
            outputString = outputString + base58[(int)singleByte % 58];
        }
        return outputString;
    }

    // ********** END HASHING HELPER METHODS

}
