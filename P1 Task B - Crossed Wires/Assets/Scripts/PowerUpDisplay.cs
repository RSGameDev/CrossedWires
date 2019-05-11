using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A class that handles the display of the Power-Ups in possession.
public class PowerUpDisplay : MonoBehaviour {

    public GameObject player;           // Reference to the Player object.
    public Text powerUpMapNum;          // References to text objects.
    public Text powerUpInvisibilityNum; 
    public Text powerUpTeleportNum;     
    public Text powerUpSpeedBoostNum;       
    public Text powerUpStrongShotNum;
    public Text powerUpFastShotNum;

    // Use this for initialization
    void Start () {

        // Error handling for if text objects cannot be found.
        if (!powerUpMapNum) { Debug.LogError(name + " is missing powerUpMapNum Text"); }
        if (!powerUpInvisibilityNum) { Debug.LogError(name + " is missing powerUpInvisibilityNum Text"); }
        if (!powerUpTeleportNum) { Debug.LogError(name + " is missing powerUpTeleportNum Text"); }
        if (!powerUpSpeedBoostNum) { Debug.LogError(name + " is missing powerUpSpeedBoostNum Text"); }
        if (!powerUpStrongShotNum) { Debug.LogError(name + " is missing powerUpStrongShotNum Text"); }
        if (!powerUpFastShotNum) { Debug.LogError(name + " is missing powerUpFastShotNum Text"); }

        // Reference setup. Allows integer variables to be outputted as a string into the text component.
        powerUpMapNum.text = player.GetComponent<PlayerInteractions>().mapNum.ToString();
        powerUpInvisibilityNum.text = player.GetComponent<PlayerInteractions>().invisibilityNum.ToString();
        powerUpTeleportNum.text = player.GetComponent<PlayerInteractions>().teleportNum.ToString();
        powerUpSpeedBoostNum.text = player.GetComponent<PlayerInteractions>().speedBoostNum.ToString();
        powerUpStrongShotNum.text = player.GetComponent<PlayerInteractions>().isStrongShot.ToString();
        powerUpFastShotNum.text = player.GetComponent<PlayerInteractions>().isFastShot.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        // Constantly updates the values of the respective references. 
        powerUpMapNum.text = player.GetComponent<PlayerInteractions>().mapNum.ToString();
        powerUpInvisibilityNum.text = player.GetComponent<PlayerInteractions>().invisibilityNum.ToString();
        powerUpTeleportNum.text = player.GetComponent<PlayerInteractions>().teleportNum.ToString();
        powerUpSpeedBoostNum.text = player.GetComponent<PlayerInteractions>().speedBoostNum.ToString();
        powerUpStrongShotNum.text = player.GetComponent<PlayerInteractions>().isStrongShot.ToString();
        powerUpFastShotNum.text = player.GetComponent<PlayerInteractions>().isFastShot.ToString();
    }
}
