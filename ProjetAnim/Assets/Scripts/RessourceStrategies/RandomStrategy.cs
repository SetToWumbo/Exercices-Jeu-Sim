using System;
using UnityEngine;
using System.Collections.Generic;

public class RandomStrategy : IStrategy
{
    public int ChooseResource(List<Ressource> ressources)
    {
        return UnityEngine.Random.Range(0, ressources.Count);
    }
}