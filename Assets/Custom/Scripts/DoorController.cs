using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class DoorController : MonoBehaviour {

    [SerializeField] private string m_level_name;

    private bool playerInDoor = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInDoor && CrossPlatformInputManager.GetButtonDown("EnterDoor") && GlobalState.Instance.PlayerCanMove)
        {
            Application.LoadLevel(m_level_name);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            playerInDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            playerInDoor = false;
        }
    }
}
