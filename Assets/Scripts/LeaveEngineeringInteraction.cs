using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveEngineeringInteraction : MonoBehaviour
{

    private KeyCode exitInteract = KeyCode.Escape;

    void Update()
    {
        // Load scene on button press
        if (Input.GetKeyDown(exitInteract))
        {
            SceneManager.LoadScene(0);
        }
    }
}
