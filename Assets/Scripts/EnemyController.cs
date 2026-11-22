
using UnityEngine;
using UnityEngine.AI;
using static EnemyController;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] patrolPoints;
    public int currentindex;
    public EnumStates currentstate;
    private float currentTime;
    public float maxtimetowait = 2f;
    public Transform personaje;
    public int Enemy_Damage = 20;

    public enum EnumStates
    {
        patrol,
        wait,
        chase,
        attack,
        DEAD
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        // 1. Automatically find the Player (Personaje) in the scene by Tag
        if (personaje == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                personaje = playerObj.transform;
            }
        }

        // 2. Automatically grab all child patrol points from Enemy_PatrolPoints
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            GameObject patrolParent = GameObject.FindGameObjectWithTag("PatrolParent");
            if (patrolParent != null)
            {
                int count = patrolParent.transform.childCount;
                patrolPoints = new Transform[count];
                for (int i = 0; i < count; i++)
                {
                    patrolPoints[i] = patrolParent.transform.GetChild(i);
                }
            }
        }
    }

    private void GotoNextpoint()
    {
        currentindex++;

        if (currentindex >= patrolPoints.Length)
        {
            currentindex = 0;
        }
        agent.SetDestination(patrolPoints[currentindex].position);
    }

    void Start()
    {
        gameObject.SetActive(true);
        GotoNextpoint();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (currentstate)
        {
            case EnumStates.patrol:
                if (agent.remainingDistance < 1)
                {
                    currentTime = 0;
                    currentstate = EnumStates.wait;
                }
                break;
            case EnumStates.wait:
                currentTime += Time.deltaTime;
                if (currentTime > maxtimetowait)
                {
                    GotoNextpoint();

                    currentstate = EnumStates.patrol;
                }
                break;
            case EnumStates.chase:
                agent.SetDestination(personaje.position);
                if (agent.remainingDistance < 0.5)
                {
                    currentTime = 0;
                    currentstate = EnumStates.attack;
                }
                break;
            case EnumStates.attack:
                if (agent.remainingDistance > 0.5)

                {
                    currentTime = 0;
                    currentstate = EnumStates.chase;
                }
                break;
        }
    }
}


