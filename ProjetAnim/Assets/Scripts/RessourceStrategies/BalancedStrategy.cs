using System.Collections.Generic;
using UnityEngine;

public class BalancedStrategy : IStrategy
{

    public BalancedStrategy() { }

    public int ChooseResource(List<Ressource> ressources)
    {
        int chosenIndex = 0;

        Villageois villager = GameObject.FindGameObjectWithTag("Player").GetComponent<Villageois>();
        float balancedValue = GetBalancedValue(villager, ressources[0]);

        for (int i = 1; i < ressources.Count; i++)
        {
            float newValue = GetBalancedValue(villager, ressources[i]);
            if (newValue > balancedValue)
            {
                chosenIndex = i;
                balancedValue = newValue;
            }
        }
        return chosenIndex;
    }

    private float GetBalancedValue(Villageois villager, Ressource ressource)
    {
        return ressource.valeur / Mathf.Pow(villager.GetDistanceTo(ressource), 2);
    }

}