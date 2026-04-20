using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Villageois : MonoBehaviour
{
    [SerializeField]
    private TMP_Text texteOr;

    [SerializeField]
    private TMP_Text textePlantes;

    [SerializeField]
    private TMP_Text texteRoches;

    private int or = 0;
    private int plantes = 0;
    private int roches = 0;
    private int numeroRessourceChoisie = -1;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (numeroRessourceChoisie == -1)
        {
            AllerVersProchaineRessource();
        }
        // Asse proche pour prendre la ressource
        else if (Vector3.Distance(transform.position, navMeshAgent.destination) < 1.4f)
        {
            TypeRessource typeRessource = GameManager.Instance.ressources[numeroRessourceChoisie].type;

            if (typeRessource == TypeRessource.Or) or++;
            else if (typeRessource == TypeRessource.Plante) plantes++;
            else if (typeRessource == TypeRessource.Roche) roches++;

            MiseAJourTextes();

            GameManager.Instance.DetruireRessource(numeroRessourceChoisie);
            AllerVersProchaineRessource();
        }
    }

    private void MiseAJourTextes()
    {
        texteOr.text = "Or: " + or;
        textePlantes.text = "Plantes: " + plantes;
        texteRoches.text = "Roches: " + roches;
    }

    private void AllerVersProchaineRessource()
    {
        List<Ressource> ressources = GameManager.Instance.ressources;

        if (ressources.Count == 0)
        {
            numeroRessourceChoisie = -1;
        }
        else
        {
            numeroRessourceChoisie = ChoisirRessource(ressources);

            Ressource ressource = ressources[numeroRessourceChoisie];
            navMeshAgent.SetDestination(ressource.transform.position);
        }
    }

    public void PickResource(IStrategy pickingStrategy, List<Ressource> ressources)
    {
        int chosenRessourceIndex = pickingStrategy.ChooseResource(this, ressources);
    }

    // TODO : Cette fonction devrait faire partie d'une des classes de votre patron Strat�gie au lieu de faire partie du villageois
    private int ChoisirRessource(List<Ressource> ressources)
    {
        return Random.Range(0, ressources.Count);
    }

    public float GetDistanceTo(Ressource ressource)
    {
        return (gameObject.transform.position - ressource.gameObject.transform.position).magnitude;
    }
}