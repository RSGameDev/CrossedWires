using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/****************************************************
 * Title: MouseLook class (adapted from)
 * Author: Joe Hocking 
 * Date: 2015
 * Page: 39
 * Book: Unity in Action with Unity 5
 ****************************************************/

// The class that controls the movement for the mouse input.
public class MouseLook : MonoBehaviour {

    public enum RotationAxes {           // Define the enum data.
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    
    public RotationAxes axes = RotationAxes.MouseXAndY; // Declare a public variable.

    public float sensitivityHor = 9.0f;  // Variable for speed of rotation on the horizontal axis.
    public float sensitivityVert = 9.0f; // Variable for speed of rotation on the vertical axis.

    public float minimumVert = -45.0f;   // Limit for how low in pitch the mouse will look to.
    public float maximumVert = 45.0f;    // Limit for how high in pitch the mouse will look up at.

    public float minimumHor = -30.0f;    // Limit how far left the mouse will rotate to.
    public float maximumHor = 30.0f;     // Limit how far right the mouse will rotate to.

    private float _rotationX = 0;        // Variable for the vertical angle.
    private float _rotationY = 0;        // Variable for the horizontal angle.
    

    void Start() {

        // Set reference.
        Rigidbody body = GetComponent<Rigidbody>();

        // A check for if the Rigidbody exist.
        if (body != null)                
            body.freezeRotation = true;  
    }

    void Update() {

        // Check if the MouseX enum is enabled.
        if (axes == RotationAxes.MouseX) {
            _rotationY -= Input.GetAxis("Mouse X") * sensitivityHor;              // Allows for rotation on the horizontal axis.
            _rotationY = Mathf.Clamp(_rotationY, minimumHor, maximumHor);         // Clamp the horizontal angle between min and max limits.
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -_rotationY, 0); // Make the local angle of the transform equal to the new y value and keeping the local x angle.                                                                                                  
                                                                                               
            // Check if the MouseX enum is enabled.
        } else if (axes == RotationAxes.MouseY) {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;             // Allows for rotation on the vertical axis.
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);       // Clamp the vertical angle between min and max limits.

            transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0); // Make the local angle of the transform equal to the new x value and keeping the local y angle.

        // The rotation for when MouseX and Y are enabled.
        } else {            
            _rotationY -= Input.GetAxis("Mouse X") * sensitivityHor;              // Allows for rotation on the horizontal axis.
            _rotationY = Mathf.Clamp(_rotationY, minimumHor, maximumHor);         // Clamp the horizontal angle between min and max limits.

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;             // Allows for rotation on the vertical axis.
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);       // Clamp the vertical angle between min and max limits.

            transform.localEulerAngles = new Vector3(_rotationX, -_rotationY, 0); // Make the local angle of the transform equal to the new x and y angles.            
        }
    }
}
