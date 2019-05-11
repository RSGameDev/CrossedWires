using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// The class that handles the operations for the initial scene of the game. 
public class SceneController : MonoBehaviour {

    public int currentSceneIndex;       // The scene index for the current scene.
    string currentSceneName;            // The name of the current scene.

    void Awake() {

        // Setting up references.
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Use this for initialization
    void Start () {
        // If the current scene is named Start Screen.
        if (currentSceneName == "Start Screen") {            
            Cursor.visible = true;                      // Make the cursor visible.
            Cursor.lockState = CursorLockMode.Confined; // Bring the cursor inside the game window.
            Cursor.lockState = CursorLockMode.None;     // Allow the cursor to be moved by the player.
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;       // Position the cursor to the centre of the screen.
    }
	
	// Update is called once per frame
	void Update () {		
	}

    // This method will exit the game. This occurs through clicking the "Exit Game" button in the Start Menu.
    public void ExitGame() {
        Application.Quit();                             // The game will close down. 
    }

    // This method will load a scene in the game.
    public void NextScene() {
        SceneManager.LoadScene(currentSceneIndex + 1);  // The game will load the following scene using the current scenes build index.
    }
}
