using System.Collections.Generic;
using UnityEngine;

public interface IStrategy
{
    public int ChooseResource(List<Ressource> ressources);
}