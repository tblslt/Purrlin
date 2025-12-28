using System;
using System.Data;
using UnityEngine;

//records the stats of any character/entity
public struct Stats
{
    public int maxHealth;
    public int physicalStat;
    public int magicStat;

    public Stats(int health, int physical, int magic)
    {
        maxHealth = health;
        physicalStat = physical;
        magicStat = magic;
    }

    public int health
    {
        get { return maxHealth; }
        set 
        {
            health = value;
            if (value < 0)
                //TODO: implement the destruction of an entity
                Console.WriteLine("health0");
        }
    }    
}
