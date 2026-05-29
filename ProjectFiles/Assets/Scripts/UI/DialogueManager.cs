using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public Text dialogueText;

    [Header("Typing")]
    public float typingSpeed = 0.03f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip typingSound;

    [Header("Player Lock")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour cameraLook;

    private Queue<string> sentences = new Queue<string>();

    public bool IsDialogueActive { get; private set; }
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private string currentSentence;

    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentSentence;
                isTyping = false;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        IsDialogueActive = true;
        dialoguePanel.SetActive(true);

        if (playerMovement != null) playerMovement.enabled = false;
        if (cameraLook != null) cameraLook.enabled = false;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            if (typingSound != null && audioSource != null && letter != ' ')
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        IsDialogueActive = false;
        dialoguePanel.SetActive(false);

        if (playerMovement != null) playerMovement.enabled = true;
        if (cameraLook != null) cameraLook.enabled = true;
    }
}