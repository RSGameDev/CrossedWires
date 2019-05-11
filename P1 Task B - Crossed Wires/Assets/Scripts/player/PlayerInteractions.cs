using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// A class for Player interactions
public class PlayerInteractions : MonoBehaviour {

    public GameObject player;                            // Reference to the Player game object.
    Rigidbody playerRB;

    PlayerMovement playerMovementScript;                 // Reference for the playerMovement script.

    public GameObject textForNoMap;                      // Reference to the textForNoMap object.
    public GameObject textForNoInvisibility;             // Reference to the textForNoInvisibility object.
    public GameObject textForNoSpeedBoost;               // Reference to the textForNoSpeedBoost object.

    GameObject mapOverlay;                               // Reference to the mapOverlay object.
    GameObject invisibilityOverlay;                      // Reference to the invisibilityOverlay object.
    GameObject gameOverScreen;                           // Reference to the gameOverScreen object.
    GameObject strongShotFx;                             // Reference to the strongShotFx object.

    AudioSource defeatedAudio;                           // Reference to the AudioSource.
    AudioSource itemAudio;                               // Reference to the AudioSource.

    [HideInInspector] public int mapNum;                 // How many map pick-ups in possesion of.
    [HideInInspector] public int invisibilityNum;        // How many invisibility pick-ups in possesion of.
    [HideInInspector] public int teleportNum;            // How many teleport pick-ups in possesion of.
    [HideInInspector] public int speedBoostNum;          // How many speed boost pick-ups in possesion of.
    public int powerUpCase;                              // The number associated with the particular power up.

    [HideInInspector] public bool isMap = false;         // Is the Map activated
    [HideInInspector] public bool isInvisible = false;   // Is invisibility activated
    [HideInInspector] public bool isSpeedBoost = false;  // Is speed boost activated
    [HideInInspector] public bool isStrongShot = false;  // Is strong shot activated
    [HideInInspector] public bool isFastShot = false;    // Is fast shot activated    

    
    void Start() {

        // Error handling for if objects cannot be found.
        if (!player) { Debug.LogError(name + " is missing Player Object"); }
        if (!textForNoMap) { Debug.LogError(name + " is missing textForNoMap Object"); }
        if (!textForNoInvisibility) { Debug.LogError(name + " is missing textForNoInvisibility Object"); }
        if (!textForNoSpeedBoost) { Debug.LogError(name + " is missing textForNoSpeedBoost Object"); }

        // Ignore the player's character controller collider with its own capsule collider.
        Physics.IgnoreCollision(GetComponent<CharacterController>(), GetComponent<CapsuleCollider>());

        // Reference setup. 
        playerMovementScript = player.GetComponent<PlayerMovement>();
        playerRB = GetComponent<Rigidbody>();
        defeatedAudio = playerMovementScript.gameOver;
        itemAudio = playerMovementScript.itemFound;

        // Reference setup. 
        mapOverlay = GameObject.Find("Map Overlay");
        invisibilityOverlay = GameObject.Find("Invisibility Overlay");
        gameOverScreen = GameObject.Find("Game Over Screen");
        strongShotFx = GameObject.Find("StrongShot ParticleSystem");
    }

    void Update() {

        // If pressing the M key and is in possession of a map pick-up and the map isn't already activated.
        if ((Input.GetKeyDown(KeyCode.M) && mapNum != 0 && isMap == false)) {          
            isMap = true;                                                       // the map has been activated
            StartCoroutine(MapActiveTime());                                    // Initiate Coroutine for how long the map is active for.
            mapNum--;                                                           // Deduct 1 from how many map pick-ups are in possession.
        // If pressing the M key and is in possession of zero map 
        } else if (Input.GetKeyDown(KeyCode.M) && mapNum == 0) {
            textForNoMap.GetComponent<Text>().enabled = true;                   // Enable the text for when the player has no map pick-up in possession. 
            StartCoroutine(ScreenTextClear());                                  // Initiate Coroutine for clearing of the latest screen message.
        }
        // If pressing the I key and is in possession of an invisibility pick-up and invisibility isn't already activated.
        if (Input.GetKeyDown(KeyCode.I) && invisibilityNum != 0 && isInvisible == false) {
            isInvisible = true;                                                 // Invisibility has been activated.    
            StartCoroutine(InvisibilityActiveTime());                           // Initiate Coroutine for how long the player will be invisible for.
            invisibilityNum--;                                                  // Deduct 1 from how many invisibility pick-ups are in possession.
        // If pressing the I key and is in possession of zero invisibility pick-ups.
        } else if (Input.GetKeyDown(KeyCode.I) && invisibilityNum == 0) {           
            textForNoInvisibility.GetComponent<Text>().enabled = true;          // Enable the text for when the player has no invisibility pick-up in possession.  
            StartCoroutine(ScreenTextClear());                                  // Initiate Coroutine for clearing of the latest screen message.       
        }
        // If pressing the left shift key and is in possession of a speed boost pick-up and a speed boost isn't already activated.
        if (Input.GetKeyDown(KeyCode.LeftShift) && speedBoostNum != 0 && isSpeedBoost == false) {
            isSpeedBoost = true;                                                // Speed boost has been activated.    
            StartCoroutine(SpeedBosstActiveTime());                             // Initiate Coroutine for how long the speed boost will be activated for.
            speedBoostNum--;                                                    // Deduct 1 from how many speed boost pick-ups are in possession.
        // If pressing the left shift key and is in possession of zero speed boost pick-ups.
        } else if (Input.GetKeyDown(KeyCode.LeftShift) && speedBoostNum == 0) {
            textForNoSpeedBoost.GetComponent<Text>().enabled = true;            // Enable the text for when the player has no speed boost pick-up in possession. 
            StartCoroutine(ScreenTextClear());                                  // Initiate Coroutine for clearing of the latest screen message. 
        }
    }

    void OnCollisionEnter(Collision collision) {    

        // If the player has a collision with an enemy object OR a tougher enemy object OR specific to the final level, a tough enemies collider attached to it.
        if (collision.gameObject.name == "Enemy Robot" || collision.gameObject.name == "Enemy Robot Tougher" || collision.gameObject.name == "Tough Enemy") {
            playerRB.constraints = RigidbodyConstraints.FreezeAll;
            GetComponentInChildren<SphereCollider>().enabled = false;           // Disable the SphereCollider for the player.
            GetComponentInChildren<PlayerDetect>().enabled = false;             // Disable the PlayerDetect script for the player.
            collision.gameObject.GetComponent<Patrol>().enemyGoToPlayer = false;// Set the enemyGoToPlayer variable for the enemy to false.
            GetComponent<PlayerMovement>().enabled = false;                     // Disable the player movement script.
            GetComponent<CharacterController>().enabled = false;                // Cease control for the player to move around.
            GetComponent<CapsuleCollider>().enabled = false;                    // Disable the players CapsuleCollider.
            defeatedAudio.Play();                                               // Play the game over audio.
            StartCoroutine(GameOverScreenRestart());                            // The game over screen will appear now.
        }
    }

    void OnTriggerEnter(Collider other) {

        // If the player's collider hits another object.
        if (other.gameObject.name == "Map") {                   // If the player hits an object named "Map". 
            powerUpCase = 1;                                    // The power up identifier for this pick-up.
            itemAudio.Play();                                   // A sound to indicate the item has been picked up.
            mapNum++;                                           // Map pick-ups in possession increase by 1. 
            other.GetComponent<ScreenInfo>().PowerUpText();     // Execute the PowerUpText function from the ScreenInfo script. To display the appropriate text for this pick-up.
        }
        if (other.gameObject.name == "Invisibility") {          // If the player hits an object named "Invisibility". 
            powerUpCase = 2;                                    // The power up identifier for this pick-up.
            itemAudio.Play();                                   // A sound to indicate the item has been picked up.
            invisibilityNum++;                                  // Invisibility pick-ups in possession increase by 1.
            other.GetComponent<ScreenInfo>().PowerUpText();     // Execute the PowerUpText function from the ScreenInfo script. To display the appropriate text for this pick-up.
        }   
        if (other.gameObject.name == "Teleport") {              // If the player hits an object named "Teleport". 
            powerUpCase = 3;                                    // The power up identifier for this pick-up.
            itemAudio.Play();                                   // A sound to indicate the item has been picked up.
            teleportNum++;                                      // Teleport pick-ups in possession increase by 1.
            other.GetComponent<ScreenInfo>().PowerUpText();     // Execute the PowerUpText function from the ScreenInfo script. To display the appropriate text for this pick-up.
        }
        if (other.gameObject.name == "Speed Boost") {           // If the player hits an object named "Speed Boost". 
            powerUpCase = 4;                                    // The power up identifier for this pick-up.
            itemAudio.Play();                                   // A sound to indicate the item has been picked up.
            speedBoostNum++;                                    // Speed boost pick-ups in possession increase by 1.
            other.GetComponent<ScreenInfo>().PowerUpText();     // Execute the PowerUpText function from the ScreenInfo script. To display the appropriate text for this pick-up.
        }        
        if (other.gameObject.name == "Strong Shot") {           // If the player hits an object named "Strong Shot". 
            powerUpCase = 5;                                    // The power up identifier for this pick-up.
            isStrongShot = true;                                // Strong shot ability is now activated.
            strongShotFx.GetComponent<ParticleSystem>().Play(); // Visuals to signify that strong shot is active
            itemAudio.Play();                                   // A sound to indicate the item has been picked up.
            other.GetComponent<ScreenInfo>().PowerUpText();     // Execute the PowerUpText function from the ScreenInfo script. To display the appropriate text for this pick-up.
        }
        if (other.gameObject.name == "Fast Shot") {             // If the player hits an object named "Fast Shot". 
            powerUpCase = 6;                                    // The power up identifier for this pick-up.
            isFastShot = true;                                  // Fast shot ability is now activated.           
            itemAudio.Play();                                   // A sound to indicate the item has been picked up.
            other.GetComponent<ScreenInfo>().PowerUpText();     // Execute the PowerUpText function from the ScreenInfo script. To display the appropriate text for this pick-up.
        } 
    }

    // Clears the screen of selected text
    IEnumerator ScreenTextClear() {
            yield return new WaitForSeconds(2f);                // Delays the function for 2 seconds.

            // Removes relevant screen information.
            textForNoMap.GetComponent<Text>().enabled = false;   
            textForNoInvisibility.GetComponent<Text>().enabled = false;
            textForNoSpeedBoost.GetComponent<Text>().enabled = false;
    }

    // Displays the game over screen before removing it then restarting the level.
    IEnumerator GameOverScreenRestart() {
        gameOverScreen.GetComponent<Image>().enabled = true;
        gameOverScreen.GetComponentInChildren<Text>().enabled = true;
        yield return new WaitForSeconds(5f);                    // Delays for 5 seconds.
        gameOverScreen.GetComponent<Image>().enabled = false;
        gameOverScreen.GetComponentInChildren<Text>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // Reloads the current level in the game.
    }

    // Displays the map for a duration of time.
    IEnumerator MapActiveTime() {
        mapOverlay.GetComponent<RawImage>().enabled = true;
        yield return new WaitForSeconds(8f);                    // Delays for 8 seconds.
        mapOverlay.GetComponent<RawImage>().enabled = false;
        isMap = false;                                          // The map has become deactivated.
    }

    // Displays the invisibility overlay for a duration of time.
    IEnumerator InvisibilityActiveTime() {
        invisibilityOverlay.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(15f);                   // Delays for 15 seconds.
        invisibilityOverlay.GetComponent<Image>().enabled = false;
        isInvisible = false;                                    // Invisibility has become deactivated.
    }

    // How long speed boost is active for.
    IEnumerator SpeedBosstActiveTime() {
        yield return new WaitForSeconds(20f);                   // Delays for 20 seconds.
        isSpeedBoost = false;                                   // Speed boost has become deactivated.
    }        
}
            
