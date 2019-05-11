using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class that handles the behaviour for the pick-ups in the game.
public class PickUps : MonoBehaviour {

    public Transform player;    // Reference to the player Transform.

	// Use this for initialization
	void Start () {

        // Ignore the player's detection range collider with its own box collider. So the pickup is only triggered when the player itself comes into contact with it.
        Physics.IgnoreCollision(player.GetComponentInChildren<SphereCollider>(), GetComponent<SphereCollider>());
	}
	
	// Update is called once per frame
	void Update () {		
	}
}
