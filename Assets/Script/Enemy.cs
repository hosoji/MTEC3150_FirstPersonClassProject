//using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //private CharacterController controller;
    public Transform player;
    //private Vector3 targetPoint;
    private Vector3 directionToPlayer;

    private Vector3 lastKnownPosition;

    private bool patrolling = true;
    private bool playerFound = false;

    public float alertDuration = 5;
    private float timeSinceAlerted = 0;
    public Transform [] waypoints;
    private Transform targetWaypoint;
    //private Vector3 targetPosition;
    private int waypointIndex = 0;

    public float viewAngle = 120;
    public float viewRange = 5;
    public float detectionRadius = 0.5f;

    public LayerMask playerLayer;

    private NavMeshAgent agent;

    //private Quaternion defaultRotation;

    //public float rotationSpeed = 5;
    //public float walkSpeed = 5;


    



    void Start()
    {
        //controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();

        
    }


    void Update()
    {
        targetWaypoint = waypoints[waypointIndex];
        

        //Debug.DrawRay(transform.position, transform.forward * 10, Color.bisque);

        //targetPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
        directionToPlayer = (player.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(directionToPlayer);

        if (patrolling)
        {
            Patrol();
        }

        if (PlayerDetected())
        {
            playerFound = true;
            patrolling = false;

            timeSinceAlerted = 0;
            lastKnownPosition = player.position;
            //Debug.Log("Player found!");
            agent.SetDestination(lastKnownPosition);

              //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rot, agent.angularSpeed * Time.deltaTime );
    
        } 

        if (playerFound)
        {
            if (timeSinceAlerted < alertDuration)
            {
                //Debug.Log("Looking for player");
                timeSinceAlerted += Time.deltaTime;
            }
            else
            {
                //Debug.Log("Returning to patrol");
                playerFound = false;
                timeSinceAlerted = 0;
                patrolling = true;
            }
            
        }

        //controller.Move(transform.forward * walkSpeed * Time.deltaTime);

        
    }

    private void Patrol()
    {
        //Debug.Log("Patrolling");
        agent.SetDestination(targetWaypoint.position);

        float dist = Vector3.Distance(transform.position, targetWaypoint.position);
        float buffer = 0.25f;

        if (dist <= buffer)
        {
    
            waypointIndex++;

            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }


        
    }

    private bool PlayerDetected()
    {
        bool result = false;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < viewAngle / 2)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, viewRange, playerLayer))
            {
                result = true;
            }
        }

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= detectionRadius)
        {
            result = true;
        }

        return result;   
    }
}
