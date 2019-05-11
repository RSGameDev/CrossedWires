using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// A class for the enemy.
public class Enemy : MonoBehaviour {

    public AudioSource enemySound;      // References to AudioSources.
    public AudioSource enemyHit;             
    public AudioSource enemyDie;
    
    public int health;                  // the enemies health.

    void Start() {

        // Error handling for if objects cannot be found.
        if (!enemySound) { Debug.LogError(name + " is missing enemySound AudioSource"); }
        if (!enemyHit) { Debug.LogError(name + " is missing enemyHit AudioSource"); }
        if (!enemyDie) { Debug.LogError(name + " is missing enemyDie AudioSource"); }                        
    }

    // Calculates the damage the enemy has taken.
    public void Damage(int damageTaken) {
        health -= damageTaken;                              // Enemies health deducted by damagetaken recieved.

        // If enemies health is more than zero.
        if (health > 0) {
            enemyHit.Play();                                // Play an enemy being hit sound.
        }

        // If the enemies health is equal to or less than zero
        if (health <= 0) {
            GetComponent<BoxCollider>().enabled = false;    // The enemy stops interacting with other colliders.
            GetComponent<Patrol>().enabled = false;         // Disable the enemies ability to move.    
            GetComponent<NavMeshAgent>().enabled = false;   // Disables the enemies ability to navigate.
            enemyDie.Play();                                // Play an enemy dieing sound.
            GetComponentInChildren<ParticleSystem>().Play();  // Visuals to signify the enemy has been defeated.
            StartCoroutine(EnemyDeath());                   // Initiate Coroutine for deactivating the enemy object. 
        }
    }

    IEnumerator EnemyDeath() {
        yield return new WaitForSeconds(1f);                // Delays the function for 1 seconds.            
        gameObject.SetActive(false);                        // The enemy object deactivates.
    }
}
