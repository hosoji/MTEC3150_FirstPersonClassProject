//using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController controller;
    public Transform player;
    private Vector3 targetPoint;
    private Vector3 directionToPlayer;

    public float viewAngle = 120;
    public float viewRange = 5;
    public float detectionRadius = 0.5f;

    public LayerMask playerLayer;

    //private Quaternion defaultRotation;

    public float rotationSpeed = 5;
    public float walkSpeed = 5;


    



    void Start()
    {
        controller = GetComponent<CharacterController>();

        
    }


    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 10, Color.bisque);

        targetPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
        directionToPlayer = (targetPoint - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(directionToPlayer);

        if (PlayerDetected())
        {
             transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rot, rotationSpeed * Time.deltaTime );
    
        } 

        controller.Move(transform.forward * walkSpeed * Time.deltaTime);

        
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
