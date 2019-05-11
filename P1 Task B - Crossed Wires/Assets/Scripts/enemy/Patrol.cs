using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/****************************************************
 * Title: Patrol class (adapted from)
 * Author: Unity Learn Manual Documentation 
 * Availability: https://docs.unity3d.com/Manual/nav-AgentPatrol.html
 ****************************************************/

// A class that enables the enemy to move around.
public class Patrol : MonoBehaviour
{

    public GameObject player;       // Reference to the Transform component.

    public Transform[] points;      // Reference to the Transform component as an array.
    private NavMeshAgent agent;     // Reference to the NavMeshAgent component.    

    public bool enemyGoToPlayer;    // Does the enemy have permission to go to the player.

    void Awake()
    {

        // Ignore the player's collider when in contact with its own box collider.
        Physics.IgnoreCollision(player.GetComponent<CharacterController>(), GetComponent<BoxCollider>());


        // Reference setup.
        agent = GetComponent<NavMeshAgent>();

    }

    // Use this for initialization
    void Start()
    {

        // Error handling for if no player object can be found.
        if (!player) { Debug.LogError(name + " is missing Player Object"); }

        // reference setup.  
        GotoNextPoint();
    }

    void GotoNextPoint()
    {


        // if points is at zero
        if (points.Length == 0)
            return;

        //Transform i = points[Random.Range(0, points.Length)];

        //transform.LookAt(i.transform.position);

        // the enemies destination is randomly choosen between the points initialised at start up. 
        agent.destination = points[Random.Range(0, points.Length)].position;
        //agent.destination = i.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if the enemy is less then 1.25f distance.
        if (agent.remainingDistance < 1.25f && !enemyGoToPlayer)
        {
            GotoNextPoint();
        }

        // if the enemy is allowed to go to the player.
        if (enemyGoToPlayer == true)
        {
            agent.SetDestination(player.transform.position);    // The destination for the enemy is set to the players position.      
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("collide");
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), other.collider);
        }
    }
}





