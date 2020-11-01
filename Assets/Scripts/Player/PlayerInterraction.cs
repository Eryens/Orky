using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterraction : MonoBehaviour {

    [HideInInspector]
    public bool canInteract = false;
    Interactible currentInteractible;

    public GameObject player;

    [HideInInspector]
    public static PlayerInterraction instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Interactible")
        {
            canInteract = true;
            currentInteractible = collider.GetComponent<Interactible>();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Interactible")
        {
            canInteract = false ;
            currentInteractible = null;
        }
    }

    private void Update()
    {
        
        if (canInteract && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            if (currentInteractible != null)
            {
                currentInteractible.OnInteractionWith();
            }
        }
    }


}
