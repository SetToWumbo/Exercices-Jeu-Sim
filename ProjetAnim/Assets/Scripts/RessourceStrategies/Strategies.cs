using System;
using System.Collections.Generic;

public enum RessourcePickingStrategy
{
    Random = 1,
    Nearest = 2,
    Balanced = 3
}

// note for self: this actually breaks the strategy pattern because 
// Execute checks the type of strategy to execute before executing it
public static class StrategyExtensions
{
    public static int Execute(this RessourcePickingStrategy strategy, List<Ressource> ressources)
    {
        switch (strategy)
        {
            case RessourcePickingStrategy.Random:
                return randomStrategy.ChooseResource(ressources);
            case RessourcePickingStrategy.Nearest:
                return nearestStrategy.ChooseResource(ressources);
            case RessourcePickingStrategy.Balanced:
                return balancedStrategy.ChooseResource(ressources);
            default:
                throw new Exception("Ressource picking type invalid");
        }
    }

    private static RandomStrategy randomStrategy = new();
    private static NearestStrategy nearestStrategy = new();
    private static RandomStrategy balancedStrategy = new();
}