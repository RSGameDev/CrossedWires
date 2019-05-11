using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// A class that loads the next level in the game.
public class Exit : MonoBehaviour {

    public Transform player;             // Reference to the player Transform.
    public GameObject sceneController;   // Reference to the sceneController GameObject.

    public Text nextLevelText;           // References to text objects.
    public Text gameCompletedText;       // References to text objects.

    GameObject finishFx;                 // Reference to the CompleteFx particle system.
    GameObject finishFx1;                // Reference to the CompleteFx particle system.      

    public AudioSource levelCompleted;   // References to AudioSources.  

    void Start() {
        finishFx = GameObject.Find("Complete Fx");      // Reference to a GameObject.
        finishFx1 = GameObject.Find("Complete Fx 1");   // Reference to a GameObject.

        // Ignore the player's detection collider with its own collider. As this game object only wants to respond to the collider on the player itself.
        Physics.IgnoreCollision(player.GetComponentInChildren<SphereCollider>(), GetComponent<BoxCollider>());
    }

    void OnTriggerEnter(Collider other) {

        other.GetComponent<PlayerMovement>().enabled = false;               // Disable the movement for the other object.  

        // If the current scene has a specific name
        if (SceneManager.GetActiveScene().name == "Level Final") {
            gameCompletedText.GetComponent<Text>().enabled = true;          // Displays screen message.
            levelCompleted.Play();                                          // Play the level completed audio.
            finishFx.GetComponent<ParticleSystem>().Play();                 // Visuals for once reached the exit.
            finishFx1.GetComponent<ParticleSystem>().Play();                
            FinishedLevel();                                                // Execute the FinishedLevel function.
        } else {
            nextLevelText.GetComponent<Text>().enabled = true;              // Displays screen message.
            levelCompleted.Play();                                          // Play the level completed audio.
            finishFx.GetComponent<ParticleSystem>().Play();                 // Visuals for once reached the exit.
            finishFx1.GetComponent<ParticleSystem>().Play();
            FinishedLevel();                                                // Execute the FinishedLevel function.
        }
    }

    void FinishedLevel() {
        StartCoroutine(Congratulations());                      
    } 

    IEnumerator Congratulations() {
                
        yield return new WaitForSeconds(4f);                                // Delay for 4 seconds.
        nextLevelText.GetComponent<Text>().enabled = false;                 // Removes screen information.  
        gameCompletedText.GetComponent<Text>().enabled = false;             // Removes screen information.    

        // If the current scene has a specific name  
        if (SceneManager.GetActiveScene().name == "Level Final") {
            Application.Quit();                                             // Quit the game.
        }
                   
        sceneController.GetComponent<SceneController>().NextScene();        // Load the next level in the game. 
    }    
}
