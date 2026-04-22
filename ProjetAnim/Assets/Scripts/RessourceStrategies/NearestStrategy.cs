using System.Collections.Generic;
using UnityEngine;

public class NearestStrategy : IStrategy
{
    public int ChooseResource(List<Ressource> ressources)
    {
        Villageois villager = GameObject.FindGameObjectWithTag("Player").GetComponent<Villageois>();
        int ressourceIndex = 0;

        float nearestDistance = villager.GetDistanceTo(ressources[0]);
        for (int i = 1; i < ressources.Count; i++)
        {
            float distance = villager.GetDistanceTo(ressources[i]);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                ressourceIndex = i;
            }
        }
        return ressourceIndex;
    }
}