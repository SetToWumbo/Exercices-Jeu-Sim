using System.Collections.Generic;
using UnityEngine;

public interface IStrategy
{
    public int ChooseResource(Villageois villager, List<Ressource> ressources);
}