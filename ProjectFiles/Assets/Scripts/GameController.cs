using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController main;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] string sceneToLoad;

    DialogueManager manager;
    DialogueTrigger trigger;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        manager = GetComponent<DialogueManager>();
        trigger = GetComponent<DialogueTrigger>();

        trigger.TriggerDialogue();
    }
}