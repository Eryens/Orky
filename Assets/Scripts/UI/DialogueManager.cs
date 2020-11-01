using GameToolkit.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Text dialogText;
    private GameObject dialogBox;
    private GameObject dialogImage;

    private Queue<string> sentences;

    public static DialogueManager instance;

    bool hasFirstSentenceBeenTyped = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }

    // Dialogue sans sprite
    public void StartDialogue (Dialogue dialogue)
    {
        dialogBox.SetActive(true);
        //Debug.Log("Starting conversation");
        dialogImage.GetComponent<Image>().enabled = false;
        GameManager.instance.TriggerDialoguePause();
        PlayerInterraction.instance.canInteract = false;
        sentences.Clear();

        if (Localization.Instance.CurrentLanguage == SystemLanguage.English)
        {
            foreach (string currentSentence in dialogue.sentencesEnglish)
            {
                sentences.Enqueue(currentSentence);
            }
        }
        else
        {
            foreach (string currentSentence in dialogue.sentences)
            {
                sentences.Enqueue(currentSentence);
            }
        }

        DisplayNextSentence();
    }

    // Useless maintenant, je garde pour le LORE
    public void StartBuggyDialog(Dialogue dialogue)
    {
        if (dialogBox == null)
        {
            CheckForDialog();
        }
        dialogBox.SetActive(true);
        //Debug.Log("Starting conversation");
        dialogImage.GetComponent<Image>().enabled = false;
        GameManager.instance.TriggerDialoguePause();
        PlayerInterraction.instance.canInteract = false;
        sentences.Clear();

        if (Localization.Instance.CurrentLanguage == SystemLanguage.English)
        {
            foreach (string currentSentence in dialogue.sentencesEnglish)
            {
                sentences.Enqueue(currentSentence);
            }
        }
        else
        {
            foreach (string currentSentence in dialogue.sentences)
            {
                sentences.Enqueue(currentSentence);
            }
        }

        DisplayNextSentence();
    }

    private void CheckForDialog()
    {
        dialogText = GameObject.Find("DialogueText").GetComponent<Text>();
        dialogBox = GameObject.Find("DialogueBox");
        dialogImage = GameObject.Find("TalkingPersonImage");
    }

    // Dialogue avec sprite
    public void StartDialogue(Dialogue dialogue, SpriteRenderer sprite)
    {
        if (sprite == null)
        {
            Debug.LogError("The dialogue was supposed to have a sprite but the given sprite was not found");
            StartDialogue(dialogue);
        }
        //Debug.Log("start dialogue");
        dialogImage.GetComponent<Image>().enabled = true;
        dialogImage.GetComponent<Image>().sprite = sprite.sprite;
        dialogBox.SetActive(true);
        //Debug.Log("Starting conversation");
        GameManager.instance.TriggerDialoguePause();
        PlayerInterraction.instance.canInteract = false;
        sentences.Clear();

        if (Localization.Instance.CurrentLanguage == SystemLanguage.English)
        {
            foreach (string currentSentence in dialogue.sentencesEnglish)
            {
                sentences.Enqueue(currentSentence);
            }
        }
        else
        {
            foreach (string currentSentence in dialogue.sentences)
            {
                sentences.Enqueue(currentSentence);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            hasFirstSentenceBeenTyped = false;
            StartCoroutine(EndDialogueCoroutine());
            EndDialogue();
            return;
        }// S'il n'y a plus de phrases

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private void Update()
    {
        if (hasFirstSentenceBeenTyped)
        {
            if (GameManager.instance.isInDialogue && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
            {
                hasFirstSentenceBeenTyped = false;
                DisplayNextSentence();
            }
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            FindObjectOfType<AudioManager>().Play("TextSound");
            yield return null;
            
        }
        yield return null;
        hasFirstSentenceBeenTyped = true;
    }

    private void EndDialogue()
    {
        dialogBox.SetActive(false);
        GameManager.instance.EndDialoguePause();
        //Debug.Log("fin du dialogue");
    }

    private IEnumerator EndDialogueCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        hasFirstSentenceBeenTyped = false;
        PlayerInterraction.instance.canInteract = true;
    }

    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
        GameObject d = GameObject.Find("UI/DialogueBox/DialogueText");
        if (d != null)
            dialogText = GameObject.Find("UI/DialogueBox/DialogueText").GetComponent<Text>();
        dialogBox = GameObject.Find("UI/DialogueBox");
        dialogImage = GameObject.Find("UI/DialogueBox/TalkingPersonImage");
        if (dialogBox != null)
            dialogBox.SetActive(false);
    }
}
