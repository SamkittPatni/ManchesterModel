using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{
    private bool canInteract = false;

    // UI Elements
    public TextMeshProUGUI interactText;

    // Control Keys
    private KeyCode interactKey = KeyCode.E;

    void Update()
    {
        // Load building scene if button us pressed when within range
        if (canInteract && Input.GetKeyDown(interactKey))
        {
            SceneManager.LoadScene(1);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        // If player is within range allow for interaction and set text
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            SetInteractText();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // If player exits range disallow interaction ad remove text
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            RemoveInteractText();
        }
    }

    // Setting text function
    private void SetInteractText()
    {
        interactText.text = "Press [E] to Enter";
    }

    // Removing text function
    private void RemoveInteractText()
    {
        interactText.text = "";
    }
}
