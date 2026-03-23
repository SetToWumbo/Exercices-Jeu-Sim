using System.Collections;
using System.IO.Enumeration;
using UnityEngine;
using UnityEngine.AI;

public class Patrouilleur : MonoBehaviour
{
    [SerializeField] private GameObject[] tableauPointsPatrouille;
    private NavMeshAgent navPatrouilleur;
    [SerializeField] private float PatrouilleDelai;
    private Coroutine patrouilleCoroutine;
    private Animator _animator;

    private System.Random random;
    private bool enPatrouille = false;

    void Start()
    {
        random = new System.Random();
        _animator = gameObject.GetComponent<Animator>();
        navPatrouilleur = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Debug.Log("Walk" + _animator.GetBool("Walk"));

        if (!enPatrouille)
        {
            if (patrouilleCoroutine != null) StopCoroutine(patrouilleCoroutine);
            enPatrouille = true;
            patrouilleCoroutine = StartCoroutine(Patrouiller(navPatrouilleur));
        }
    }

    IEnumerator Patrouiller(NavMeshAgent navigateur)
    {
        _animator.SetBool("Walk", true);
        int index = random.Next(tableauPointsPatrouille.Length);
        Vector3 destination = tableauPointsPatrouille[index].transform.position;

        navigateur.SetDestination(destination);
        while (navigateur.pathPending || navigateur.remainingDistance > navigateur.stoppingDistance)
        {
            // Debug.Log("Dans le premier while");
            yield return null;
        }

        // Debug.Log("Sortis du premier while");

        _animator.SetBool("Walk", false);
        yield return new WaitForSecondsRealtime(PatrouilleDelai);
        enPatrouille = false;
    }
}