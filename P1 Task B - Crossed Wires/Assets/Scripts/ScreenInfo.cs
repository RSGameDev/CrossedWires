using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A class that displays Game Text on the screen
public class ScreenInfo : MonoBehaviour {

    public Transform player;        // Reference to the player Transform.
    
    public Text displayTextIntro;   // References to text objects.
    public Text displayText1;       
    public Text displayText2;
    public Text displayText3;
    public Text mapText;            // Reference to the pick-up text objects.
    public Text invisibleText;
    public Text teleportText;
    public Text speedBoostText;
    public Text strongShotText;
    public Text fastShotText;

    int powerUp;                    // The identifier number for the pick-up in use.
    int narration;                  // The identifier number for the narration text to be displayed.
    bool isIntroInformation = false;// Set the introduction text to be displayed for the level to false.


    void Start() {
        
        // Configuring pick-up objects in the scene.
        if (gameObject.tag == "Pick-up") {
            // Ignore the player's detect SphereCollider with its own SphereCollider. As the pick-up only needs to collide with the player's collider itself and not its detect range collider.
            Physics.IgnoreCollision(player.GetComponentInChildren<SphereCollider>(), GetComponent<SphereCollider>());
        // Configuring the narration objects in the scene.
        } else if (gameObject.tag == "narration"){
            // Ignore the player's detect SphereCollider with its own BoxCollider. As the narration game object collider only needs to collide with the player's collider itself and not its detect range collider.
            Physics.IgnoreCollision(player.GetComponentInChildren<SphereCollider>(), GetComponent<BoxCollider>());
        }

        // Check if this boolean is false. 
        if (!isIntroInformation) {
            narration = 1;                // The narration identifier for this specific narration.
            Invoke("NarrationText", 1);   // After 1 second initiate the NarrationText method.            
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && gameObject.name == "Narrate") {     // If the player hits an object named "Narrate". 
            narration = 2;                                                          // The narration identifier for this specific narration.
            NarrationText();                                                        // Execute the NarrationText() function.
        }
        if (other.gameObject.tag == "Player" && gameObject.name == "Narrate 2") {   // If the player hits an object named "Narrate 2". 
            narration = 3;                                                          // The narration identifier for this specific narration.
            NarrationText();                                                        // Execute the NarrationText() function.
        }
        if (other.gameObject.tag == "Player" && gameObject.name == "Narrate 3") {   // If the player hits an object named "Narrate 3". 
            narration = 4;                                                          // The narration identifier for this specific narration.
            NarrationText();                                                        // Execute the NarrationText() function.
        }        

        StartCoroutine(ScreenTextClear());                                          // Initiate Coroutine for clearing of the latest screen message.         
    }

    // This function clears any previous narration text and displays the correct text to be displayed at that point in time.
    void NarrationText() {
        displayTextIntro.GetComponentInParent<Image>().enabled = false;             // Disables the image associated with this game object.
        displayTextIntro.GetComponent<Text>().enabled = false;                      // Disables the text associated with this game object.
        displayText1.GetComponentInParent<Image>().enabled = false;                 // Disables the image associated with this game object.
        displayText1.GetComponent<Text>().enabled = false;                          // Disables the text associated with this game object.
        displayText2.GetComponentInParent<Image>().enabled = false;                 // Disables the image associated with this game object.
        displayText2.GetComponent<Text>().enabled = false;                          // Disables the text associated with this game object.
        displayText3.GetComponentInParent<Image>().enabled = false;                 // Disables the image associated with this game object.
        displayText3.GetComponent<Text>().enabled = false;                          // Disables the text associated with this game object.     

        // A switch case using the narration identifier obtained after the player collides with a narration game object collider.
        switch (narration) {
            case 1:
                displayTextIntro.GetComponentInParent<Image>().enabled = true;      // Displays the image and message on the screen
                displayTextIntro.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 2:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                displayText1.GetComponentInParent<Image>().enabled = true;          // Displays background and message on the screen
                displayText1.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 3:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                displayText2.GetComponentInParent<Image>().enabled = true;          // Displays background and message on the screen
                displayText2.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 4:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                displayText3.GetComponentInParent<Image>().enabled = true;          // Displays background and message on the screen
                displayText3.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;            
        }
    }

    // This function clears any previous pick-up text and displays the correct text to be displayed at that point in time.
    public void PowerUpText() {

        // Reference setup.
        powerUp = player.GetComponent<PlayerInteractions>().powerUpCase;

        GetComponent<MeshRenderer>().enabled = false;                               // Disable the mesh renderer for the game object.
        GetComponent<AudioSource>().enabled = false;                                // Disable the audio source for the game object.

        mapText.GetComponentInParent<Image>().enabled = false;                      // Disables the image associated with this game object.
        mapText.GetComponent<Text>().enabled = false;                               // Disables the text associated with this game object.
        invisibleText.GetComponentInParent<Image>().enabled = false;                // Disables the image associated with this game object.
        invisibleText.GetComponent<Text>().enabled = false;                         // Disables the text associated with this game object.
        teleportText.GetComponentInParent<Image>().enabled = false;                 // Disables the image associated with this game object.
        teleportText.GetComponent<Text>().enabled = false;                          // Disables the text associated with this game object.
        speedBoostText.GetComponentInParent<Image>().enabled = false;               // Disables the image associated with this game object.
        speedBoostText.GetComponent<Text>().enabled = false;                        // Disables the text associated with this game object.
        strongShotText.GetComponentInParent<Image>().enabled = false;               // Disables the image associated with this game object.
        strongShotText.GetComponent<Text>().enabled = false;                        // Disables the text associated with this game object.
        fastShotText.GetComponentInParent<Image>().enabled = false;                 // Disables the image associated with this game object.
        fastShotText.GetComponent<Text>().enabled = false;                          // Disables the text associated with this game object.

        // A switch case using the powerup identifier obtained after the player collides with a pick-up game object collider.
        switch (powerUp) {
            case 1:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                mapText.GetComponentInParent<Image>().enabled = true;               // Displays background and message on the screen
                mapText.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 2:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                invisibleText.GetComponentInParent<Image>().enabled = true;         // Displays background and message on the screen
                invisibleText.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 3:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                teleportText.GetComponentInParent<Image>().enabled = true;          // Displays background and message on the screen
                teleportText.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 4:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                speedBoostText.GetComponentInParent<Image>().enabled = true;        // Displays background and message on the screen
                speedBoostText.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 5:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                strongShotText.GetComponentInParent<Image>().enabled = true;        // Displays background and message on the screen
                strongShotText.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
            case 6:
                GetComponent<Collider>().enabled = false;                           // Disable the collider so this action will not occur again.
                fastShotText.GetComponentInParent<Image>().enabled = true;          // Displays background and message on the screen
                fastShotText.GetComponent<Text>().enabled = true;
                StartCoroutine(ScreenTextClear());                                  // Run the start coroutine ScreenTextClear().
                break;
        }
    }

    // This IEnemurator clears the text for the appropriate game object through the use of switch statements.
    IEnumerator ScreenTextClear() {

        yield return new WaitForSeconds(5f);                                        // Delays the function for 5 seconds.
        displayTextIntro.GetComponentInParent<Image>().enabled = false;             // Remove the background and message on the screen for the introduction text.
        displayTextIntro.GetComponent<Text>().enabled = false;

        // Check if the game object has the narration tag attached to it.
        if (gameObject.tag == "narration") {

            // A switch case using the narration identifier obtained after the player collides with a narration game object collider.
            switch (narration) {                
                case 2:                
                    yield return new WaitForSeconds(5f);
                    displayText1.GetComponentInParent<Image>().enabled = false;     // Displays background and message on the screen
                    displayText1.GetComponent<Text>().enabled = false;
                    break;
                case 3:
                    yield return new WaitForSeconds(5f);
                    displayText2.GetComponentInParent<Image>().enabled = false;     // Displays background and message on the screen
                    displayText2.GetComponent<Text>().enabled = false;
                    break;
                case 4:
                    yield return new WaitForSeconds(5f);
                    displayText3.GetComponentInParent<Image>().enabled = false;     // Displays background and message on the screen
                    displayText3.GetComponent<Text>().enabled = false;
                    break;
            }
        }

        // If the tag is of Pick-up
        if (gameObject.tag == "Pick-up") {

            // A switch case using the powerup identifier obtained after the player collides with a pick-up game object collider.
            switch (powerUp) {
                case 1:
                    yield return new WaitForSeconds(3f);                            // Delays for 3 seconds.
                    mapText.GetComponentInParent<Image>().enabled = false;          // Removes screen information.
                    mapText.GetComponent<Text>().enabled = false;
                    Destroy(gameObject);                                            // Destroys the pick-up game object.
                    break;
                case 2:
                    yield return new WaitForSeconds(3f);                            // Delays for 3 seconds.
                    invisibleText.GetComponentInParent<Image>().enabled = false;    // Removes screen information.
                    invisibleText.GetComponent<Text>().enabled = false;
                    Destroy(gameObject);                                            // Destroys the pick-up game object.
                    break;
                case 3:
                    yield return new WaitForSeconds(3f);                            // Delays for 3 seconds.
                    teleportText.GetComponentInParent<Image>().enabled = false;     // Removes screen information.
                    teleportText.GetComponent<Text>().enabled = false;
                    Destroy(gameObject);                                            // Destroys the pick-up game object.
                    break;
                case 4:
                    yield return new WaitForSeconds(3f);                            // Delays for 3 seconds.
                    speedBoostText.GetComponentInParent<Image>().enabled = false;   // Removes screen information.
                    speedBoostText.GetComponent<Text>().enabled = false;
                    Destroy(gameObject);                                            // Destroys the pick-up game object.    
                    break;
                case 5:
                    yield return new WaitForSeconds(3f);                            // Delays for 3 seconds.
                    strongShotText.GetComponentInParent<Image>().enabled = false;   // Removes screen information.
                    strongShotText.GetComponent<Text>().enabled = false;
                    Destroy(gameObject);                                            // Destroys the pick-up game object.
                    break;
                case 6:
                    yield return new WaitForSeconds(3f);                            // Delays for 3 seconds.
                    fastShotText.GetComponentInParent<Image>().enabled = false;     // Removes screen information.
                    fastShotText.GetComponent<Text>().enabled = false;
                    Destroy(gameObject);                                            // Destroys the pick-up game object.
                    break;
            }
        }       
    }
}
