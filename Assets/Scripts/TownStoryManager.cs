using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownStoryManager : MonoBehaviour {

    public Dialogue dialogue;

    [HideInInspector]
    public SpriteRenderer sprite;

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //DialogueManager.instance.StartDialogue(dialogue, sprite);
        StartCoroutine(StartDialogue());
    }

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1.0f);
        if (StoryManager.instance.GetLevel() == 0)
        {
            FindObjectOfType<AudioManager>().PlayTheme("TownTheme1");
        }
        else
        {
            FindObjectOfType<AudioManager>().PlayTheme("TownTheme2");
        }
        DialogueManager.instance.StartDialogue(dialogue, sprite);
    }
}
