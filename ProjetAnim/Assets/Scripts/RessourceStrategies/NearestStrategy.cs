using System.Collections.Generic;
using UnityEngine;

public class NearestStrategy : IStrategy
{
    public int ChooseResource(Villageois villager, List<Ressource> ressources)
    {
        int ressourceIndex = 0;

        float nearestDistance = villager.GetDistanceTo(ressources[0]);
        for (int i = 1; i < ressources.Count; i++)
        {
            ressourceIndex = (villager.GetDistanceTo(ressources[i]) < nearestDistance) ? i : ressourceIndex;
        }
        return ressourceIndex;
    }
}