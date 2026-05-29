using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GateTransition : MonoBehaviour
{
    [Header("Destination")]
    public Transform destinationPoint;

    [Header("UI")]
    public Image fadeImage;
    public Text pressEnterPrompt;

    [Header("Settings")]
    public float fadeDuration = 1f;
    public GameObject player;

    private bool playerInRange = false;
    private bool isTransitioning = false;
    private PlayerController playerController;

    private void Start()
    {
        // Ensure screen starts visible
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0;
            fadeImage.color = c;
        }

        if (pressEnterPrompt != null)
            pressEnterPrompt.text = "";
    }

    private void Update()
    {
        if (playerInRange && !isTransitioning && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        playerController.isMoving = false;

        isTransitioning = true;
        pressEnterPrompt.text = "";

        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerController>().isMoving = false;
        player.GetComponent<PlayerController>().lastMoveDir = new Vector2(1,0);

        yield return StartCoroutine(Fade(0, 1));

        player.transform.position = destinationPoint.position;

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(Fade(1, 0));

        player.GetComponent<PlayerController>().enabled = true;

        isTransitioning = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            player = collision.gameObject;

            playerController = player.GetComponent<PlayerController>();

            if (pressEnterPrompt != null)
                pressEnterPrompt.text = "Press Spacebar To Enter";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            pressEnterPrompt.text = "";
        }
    }
}