using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour {

    private Dictionary<string, List<string>> conversations;
    [SerializeField] private GameObject textBox1;
    private TypeTextBehaviour typeText1;
    [SerializeField]
    private TextAsset dialogueXml;

    private bool textBox1Done = true;

    private List<string> currentDialogue;

    // Use this for initialization
    void Start ()
    {
        typeText1 = textBox1.GetComponentInChildren<TypeTextBehaviour>();
        // Load the dialogue for the scene
        conversations = DialogueLoader.LoadDialogue(dialogueXml.text);

        textBox1.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (textBox1Done)
        {
            GlobalState.Instance.PlayerCanMove = true;
        }
	}

    public void showDialogue(string id, bool freeze)
    {
        StartCoroutine(showDialogueHelper(id, freeze));
    }

    IEnumerator showDialogueHelper(string id, bool freeze)
    {
        
        textBox1.SetActive(true);
        yield return new WaitForEndOfFrame();
        if (freeze)
            GlobalState.Instance.PlayerCanMove = false;
        textBox1Done = false;
        currentDialogue = conversations[id];
        foreach (string item in currentDialogue)
        {
            typeText1.EnqueueText(item);
        }
        typeText1.ProcessQueueNext();
    }

    public void showDialogueComplete(TypeTextBehaviour typeText)
    {
        if(typeText1 == typeText)
        {
            textBox1.SetActive(false);
            textBox1Done = true;
        }
    }
}
