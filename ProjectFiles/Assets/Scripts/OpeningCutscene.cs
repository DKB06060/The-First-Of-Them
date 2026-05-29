using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterDoor : MonoBehaviour
{
    [Header("UI")]
    public Image fadeImage;
    public Text promptText;

    [Header("Dialogue")]
    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    [Header("Player")]
    public GameObject player;
    public MonoBehaviour playerMovement;
    public MonoBehaviour cameraScript;

    [Header("Settings")]
    public float fadeDuration = 1f;
    public KeyCode interactKey = KeyCode.Space;

    [Header("Script References")]
    public MapSelectionManager mapSelectionManager;

    private bool playerInRange = false;
    private bool isRunning = false;

    private void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0;
            fadeImage.color = c;
        }

        if (promptText != null)
            promptText.text = "";
    }

    private void Update()
    {
        if (playerInRange && !isRunning && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(CutsceneSequence());
        }
    }

    IEnumerator CutsceneSequence()
    {
        player.GetComponent<PlayerController>().isMoving = false;

        isRunning = true;
        promptText.text = "";

        if (playerMovement != null) playerMovement.enabled = false;
        if (cameraScript != null) cameraScript.enabled = false;

        yield return StartCoroutine(Fade(0, 1));

        if (dialogueManager != null && dialogue != null)
        {
            dialogueManager.StartDialogue(dialogue);

            while (dialogueManager.IsDialogueActive)
            {
                yield return null;
            }
        }

        if (mapSelectionManager != null)
        {
            mapSelectionManager.ShowMap();
        }

        isRunning = false;

    }

    IEnumerator Fade(float start, float end)
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(start, end, t / fadeDuration);
            c.a = a;
            fadeImage.color = c;
            yield return null;
        }

        c.a = end;
        fadeImage.color = c;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;

            if (promptText != null)
                promptText.text = "Press Space to Enter";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (promptText != null)
                promptText.text = "";
        }
    }
}