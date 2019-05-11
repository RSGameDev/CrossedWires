using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class that controls Player movement
public class PlayerMovement : MonoBehaviour {

    public GameObject player;                       // Reference to the Player object.
    PlayerInteractions playerInteractionsScript;    // Reference for the playerInteractions script.

    private CharacterController charController;     // Reference for the Character Controller component.
    private Vector3 move;                           // Reference for Vector3 values.

    private AudioSource[] sounds;                   // References to an AudioSource array.
    public AudioSource footSteps;                   // References to AudioSources.
    public AudioSource teleport;
    public AudioSource speedBoost;
    public AudioSource gameOver;
    public AudioSource itemFound;

    [HideInInspector] public int teleportNum;       // A reference variable.
    [HideInInspector] public bool isSpeedBoost;     // A reference variable.

    public Transform[] points;                      // Reference to the Transform component as an array.

    int speedBoostValue = 2;                        // When speed boost is activated the player can move double the normal distance.

    // Use this for initialization
    void Start() {

        // Error handling for if objects cannot be found.
        if (!footSteps) { Debug.LogError(name + " is missing footSteps AudioSource"); }
        if (!teleport) { Debug.LogError(name + " is missing teleport AudioSource"); }
        if (!speedBoost) { Debug.LogError(name + " is missing speedBoost ObjectAudioSource"); }
        if (!gameOver) { Debug.LogError(name + " is missing gameOver AudioSource"); }
        if (!itemFound) { Debug.LogError(name + " is missing itemFound AudioSource"); }
        if (!player) { Debug.LogError(name + " is missing player Object"); }

        // Reference setup. 
        sounds = GetComponents<AudioSource>();
        footSteps = sounds[0];
        teleport = sounds[1];
        speedBoost = sounds[2];
        gameOver = sounds[3];
        itemFound = sounds[4];

        // Reference setup. 
        charController = GetComponent<CharacterController>();

        // Reference setup. 
        playerInteractionsScript = player.GetComponent<PlayerInteractions>();
        teleportNum = playerInteractionsScript.teleportNum;

        // Reference setup. 
        playerInteractionsScript = player.GetComponent<PlayerInteractions>();
        isSpeedBoost = playerInteractionsScript.isSpeedBoost;        
    }     

    // Update is called once per frame
    void Update() {
        // Reference setup. To constantly update the value in the respective references. 
        teleportNum = playerInteractionsScript.teleportNum;        
        isSpeedBoost = playerInteractionsScript.isSpeedBoost;

        // If pressing the W key and speed boost is not activated.
        if (Input.GetKeyDown(KeyCode.W) && isSpeedBoost == false) {
            move = transform.TransformDirection(Vector3.forward);   // Setup reference.
            charController.Move(move);                              // Move forward.
            footSteps.Play();                                       // Play the foot steps audio.
        // If pressing the W key and speed boost is activated.
        } else if (Input.GetKeyDown(KeyCode.W) && isSpeedBoost == true) {
            move = transform.TransformDirection(Vector3.forward * speedBoostValue); // Setup reference including speed boost variable.
            charController.Move(move);                              // Move forward with speed boost.
            speedBoost.Play();                                      // Play the speed boost audio.    
        }

        // If pressing the S key and speed boost is not activated.
        if (Input.GetKeyDown(KeyCode.S) && isSpeedBoost == false) {
            move = transform.TransformDirection(Vector3.back);      // Setup reference.
            charController.Move(move);                              // Move Backwards.
            footSteps.Play();                                       // Play the foot steps audio.
        // If pressing the S key and speed boost is activated.
        } else if (Input.GetKeyDown(KeyCode.S) && isSpeedBoost == true) {
            move = transform.TransformDirection(Vector3.back * speedBoostValue);    // Setup reference including speed boost variable.
            charController.Move(move);                              // Move backwards with speed boost.
            speedBoost.Play();                                      // Play the speed boost audio. 
        }

        // If pressing the A key
        if (Input.GetKeyDown(KeyCode.A)) {
            transform.Rotate(0, -90, 0);        // Rotate the player to their left by 90 degrees.                       
        }

        // If pressing the D key
        if (Input.GetKeyDown(KeyCode.D)) {
            transform.Rotate(0, 90, 0);         // Rotate the player to their right by 90 degrees.  
        }

        // If pressing the T key and is in possession of a teleport pick-up.
        if (Input.GetKeyDown(KeyCode.T) && teleportNum != 0) {
            TeleportPlayerToRandomPosition();   // Initiate the teleport to random position function.
        }
    }

    // Teleport the player to a random set position in the level.
    void TeleportPlayerToRandomPosition() {
        transform.position = points[Random.Range(0, points.Length)].position;       // Setup reference.
        teleport.Play();                        // Play the teleport audio. 
        playerInteractionsScript.teleportNum--; // Access the teleport pick-ups in possession variable in playerInteractions script and decrease by 1.
    }
}

        
