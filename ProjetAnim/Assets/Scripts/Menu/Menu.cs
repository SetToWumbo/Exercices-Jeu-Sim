using UnityEngine;

public class Menu : MonoBehaviour
{
    private Villageois villageois;
    void Start()
    {
        villageois = GameObject.FindGameObjectWithTag("Player").GetComponent<Villageois>();
    }

    public void UseRandomStrategy()
    {
        villageois.changeStrategy(RessourcePickingStrategy.Random);
    }
    public void UseNearestStrategy()
    {
        villageois.changeStrategy(RessourcePickingStrategy.Nearest);
    }
    public void UseBalancedStrategy()
    {
        villageois.changeStrategy(RessourcePickingStrategy.Balanced);
    }
}