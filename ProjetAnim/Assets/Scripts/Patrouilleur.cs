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
        _animator.SetBool("Walk", true);
        navPatrouilleur = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Debug.Log("Walk" + _animator.GetBool("Walk"));

        if (!enPatrouille)
        {
            if (patrouilleCoroutine != null) StopCoroutine(patrouilleCoroutine);
            enPatrouille = true;
            patrouilleCoroutine = StartCoroutine(Patrouiller(navPatrouilleur));
        }
    }

    IEnumerator Patrouiller(NavMeshAgent navigateur)
    {
        int index = random.Next(tableauPointsPatrouille.Length);
        
        Vector3 destination = tableauPointsPatrouille[index].transform.position;

        navigateur.SetDestination(destination);
        yield return new WaitForSeconds(0.03f);
        if (navigateur.pathPending)
        {
            Debug.Log("remaining");
            while (navigateur.remainingDistance > .5f)
            {
                Debug.Log("Dans le premier while");
                yield return null;
            }
            Debug.Log("Sortis du premier while");
        }
        _animator.SetBool("Walk", false);


        yield return new WaitForSecondsRealtime(PatrouilleDelai);

        enPatrouille = false;
    }
}