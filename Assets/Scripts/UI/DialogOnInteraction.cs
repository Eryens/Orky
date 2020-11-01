using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnInteraction : Interactible {

    public Dialogue dialogue;

    [HideInInspector]
    public SpriteRenderer sprite;

    public bool showSpriteInDialog;

    private Player player;

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void OnInteractionWith()
    {

        if (showSpriteInDialog)
        {
            DialogueManager.instance.StartDialogue(dialogue, sprite);
        }
            
        else
            DialogueManager.instance.StartDialogue(dialogue);
    }
}
