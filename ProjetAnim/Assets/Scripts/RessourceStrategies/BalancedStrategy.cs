using System.Collections.Generic;
using UnityEngine;

public class BalancedStrategy : IStrategy
{
    int chosenIndex = 0;
    float balancedValue;

    public BalancedStrategy() { }

    public int ChooseResource(Villageois villager, List<Ressource> ressources)
    {
        balancedValue = GetBalancedValue(villager, ressources[0]);
        for (int i = 1; i < ressources.Count; i++)
        {
            float newValue = GetBalancedValue(villager, ressources[i]);
            chosenIndex = (newValue > balancedValue) ? i : chosenIndex;
        }

        return chosenIndex;
    }

    private float GetBalancedValue(Villageois villager, Ressource ressource)
    {
        return ressource.valeur / Mathf.Pow(villager.GetDistanceTo(ressource), 2);
    }

}