using Stratis.SmartContracts;
using System;

public class ZombieFactory : SmartContract
{
    public int dnaDigits = 16;

    public Double DnaModulus
    {
        get
        {
            return Math.Pow(10, dnaDigits);
        }
    }



    public ZombieFactory(ISmartContractState smartContractState) : base(smartContractState)
    {
        
    }

}