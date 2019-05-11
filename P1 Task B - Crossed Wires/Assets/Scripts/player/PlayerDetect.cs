using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/****************************************************
* Title: Enemy Sight class (adapted from)
* For this script: From lines 40 to 64.
* Author: Official Unity Youtube channel  
* Availability: https://www.youtube.com/watch?v=mBGUY7EUxXQ
****************************************************/

// The class that allows the player to detect when the enemy is in field of view.
public class PlayerDetect : MonoBehaviour {
      
    SphereCollider col;                 // Reference to the player's detection SphereCollider.                         

    float fieldOfViewAngle = 110f;      // The field of view the player has.
    private bool isPlayerInvisible;     // Is the player using the Invisibility pick-up.
    
    void Awake() {
        // Setting up a reference.
        col = GetComponentInChildren<SphereCollider>();
    }

    // Use this for initialization
    void Start () {        
    }
	
	// Update is called once per frame
	void Update () {
        // Setting up a reference. Checks every frame for any changes to the variable from the PlayerInteraction script.
        isPlayerInvisible = gameObject.GetComponentInParent<PlayerInteractions>().isInvisible;
        Debug.Log("detectscript");
    }

    /**********************************************************************************
    * Adapted code using the Enemy Sight class from Unity's official Youtube channel
    ***********************************************************************************/

    void OnTriggerStay(Collider other) {

        // Check if the other collider has the enemy tag attached to it AND the player is not invisible.
        if ((other.gameObject.tag == "enemy") && isPlayerInvisible == false) {

            // Setting up the references
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            // Check if the enemy is in the field of view.
            if (angle < fieldOfViewAngle * 0.5f) {

                RaycastHit hit;

                // Check if the raycast hits anything within its detection range.
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)) {
                    
                    // If the collider that is hit by the raycast has the enemy tag attached to it.
                    if (hit.collider.gameObject.tag == "enemy") {
                        hit.collider.gameObject.GetComponentInParent<Patrol>().enemyGoToPlayer = true;  // Indicate to the Patrol script for this enemy. That it can navigate towards the player.
                    }  
                }               
            } 
        } 
    }

    /*End of adapted code**************************************************************/

    // When a collider leaves the player detection collider.
    void OnTriggerExit(Collider other) {
        // Check if the other collider has the enemy tag attached to it AND the player is not invisible.
        if ((other.gameObject.tag == "enemy") && isPlayerInvisible == false) {
            other.gameObject.GetComponent<Patrol>().enemyGoToPlayer = false; // Set the enemyGoToPlayer variable to false.
        }
    }
   
}