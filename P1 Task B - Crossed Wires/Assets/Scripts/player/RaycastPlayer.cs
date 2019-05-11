using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A class that controls the Player shooting mechanics
public class RaycastPlayer : MonoBehaviour {

    public GameObject player;                                   // Reference to the player object.
    private PlayerInteractions playerInteractionsScript;        // Reference for the playerInteractions script.

    public GameObject textForNoAmmo;    // Reference to the textForNoAmmo object.
    private Camera cameraObject;        // Reference to the camera object.

    public Image ammoReloadBar;         // References to an Image.

    private AudioSource[] sounds;       // References to an AudioSource array.
    public AudioSource normalShot;      // References to AudioSources.
    public AudioSource strongShot;           

    private bool isStrongShot;          // Is strong shot activated.
    private bool isFastShot;            // Is fast shot activated.
    bool isReloadedComplete = true;     // Is the ammo reload bar full.

    float reloadMetre = 100;            // The size of the ammo reload bar.

    [HideInInspector] public int gunDamage = 100;   // The damage each shot inflicts.


	// Use this for initialization
	void Start () {

        // Error handling for if objects cannot be found.
        if (!player) { Debug.LogError(name + " is missing player Object"); }
        if (!ammoReloadBar) { Debug.LogError(name + " is missing ammoReloadBar Image"); }
        if (!textForNoAmmo) { Debug.LogError(name + " is missing textForNoAmmo Object"); }
        if (!normalShot) { Debug.LogError(name + " is missing normalShot AudioSource"); }
        if (!strongShot) { Debug.LogError(name + " is missing strongShot AudioSource"); }

        // Reference setup. 
        sounds = GetComponents<AudioSource>();  
        normalShot = sounds[0];
        strongShot = sounds[1];

        // Reference setup. 
        cameraObject = GetComponent<Camera>();
        playerInteractionsScript = player.GetComponent<PlayerInteractions>();
        isFastShot = playerInteractionsScript.isFastShot;

        // Reference setup. 
        playerInteractionsScript = player.GetComponent<PlayerInteractions>();
        isStrongShot = playerInteractionsScript.isStrongShot;
    }
	
	// Update is called once per frame
	void Update () {

        // Reference setup. To constantly update the value in the respective references. 
        isFastShot = playerInteractionsScript.isFastShot;
        isStrongShot = playerInteractionsScript.isStrongShot;

        // If pressing the Space key and the reload bar is full
        if (Input.GetKeyDown(KeyCode.Space) && reloadMetre == 100) {            
            if (isStrongShot == false) {        // If strong shot is not active.
                normalShot.Play();              // Play the normal shot audio.
            } else if (isStrongShot == true) {  // If strong shot is active.
                strongShot.Play();              // Play the strong shot audio.
            }
            
            reloadMetre = 0;                    // The reload metre is now set to zero   

            // Setup reference
            Vector3 rayStart = cameraObject.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // if the raycast in the forward direction...
            if (Physics.Raycast(rayStart, cameraObject.transform.forward, out hit)) {

                Enemy enemy = hit.collider.GetComponent<Enemy>();   // Setup reference.
                if (enemy != null && isStrongShot == false) {       // If the raycast detects an object with the Enemy script and strong shot is not activated.
                    enemy.Damage(gunDamage);                        // Deal normal damage.
                } else if (enemy != null && isStrongShot == true) { // if the raycast detects no object with the Enemy script and strong shot is not activated.
                    gunDamage = 300;                                // The damage dealt is altered now strong shot is active.                   
                    enemy.Damage(gunDamage);                        // Deal strong shot damage.
                } 
            }
            // Initiate the gun reload method after 1 second then reoccur every 0.1 of a second.
            InvokeRepeating("GunReloadSpeed", 1, .1f);  
        } 
        
        // If pressing the Space key and the gun is still reloading.
        if (Input.GetKeyDown(KeyCode.Space) && isReloadedComplete == false) {
            textForNoAmmo.GetComponent<Text>().enabled = true;
            StartCoroutine(ScreenTextClear());                          // Initiate Coroutine for clearing of the latest screen message.
        }		
	}

    // Clears the screen of selected text
    IEnumerator ScreenTextClear() {
        yield return new WaitForSeconds(2f);                            // Delays for 2 seconds.
        textForNoAmmo.GetComponent<Text>().enabled = false;
    }

    // How long it takes to reload the weapon
    void GunReloadSpeed() {
        isReloadedComplete = false;         // Can the player shoot their weapon           

        // If the fast shot ability is activated.
        if (isFastShot == true) {       
            reloadMetre += 6.25f;           // The weapon can reload at a faster rate.
            ammoReloadBar.rectTransform.localScale = new Vector3(reloadMetre / 100, 1, 1);  // Using the localScale take on a new value each time the function is called. 

            // If the reload metre is 100 or more than.
            if (reloadMetre >= 100) {
                reloadMetre = 100;          // Set reload metre to 100.
                isReloadedComplete = true;  // The weapon is reloaded.
                CancelInvoke();             // Cease the repeating of the method.
            }

        // Otherwise
        } else {
            reloadMetre += 2f;              // The weapon is at it's default of 2f.
            ammoReloadBar.rectTransform.localScale = new Vector3(reloadMetre / 100, 1, 1);  // Using the localScale take on a new value each time the function is called. 

            // If the reload metre is 100 or more than.
            if (reloadMetre >= 100) {
                reloadMetre = 100;          // Set reload metre to 100.
                isReloadedComplete = true;  // The weapon is reloaded.
                CancelInvoke();             // Cease the repeating of the method.
            }
        }        
    }    
}
