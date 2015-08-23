using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueActivateBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject dialogueControllerObject;
    [SerializeField] private string dialogueId = "";
    [SerializeField] private bool activateOnce = true;
    [SerializeField] private bool freezeOnTrigger = false;

    private DialogueController controller;
    
    private bool activated = false;

    // Use this for initialization
    void Start()
    {
        if (dialogueControllerObject == null)
            dialogueControllerObject = GameObject.Find("DialogueController");
        controller = dialogueControllerObject.GetComponent<DialogueController>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isActiveAndEnabled) return;
        if ((activated && activateOnce) || !collider.gameObject.tag.Equals("Player")) return;

        controller.showDialogue(dialogueId, freezeOnTrigger);
        activated = true;
    }

}

