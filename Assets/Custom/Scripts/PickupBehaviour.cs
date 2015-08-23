using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PickupBehaviour : MonoBehaviour {
	private bool playerIn;
	
	// Update is called once per frame
	void Update () {
		if (playerIn && CrossPlatformInputManager.GetButtonDown("EnterDoor") && GlobalState.Instance.PlayerCanMove)
		{
			GlobalState.Instance.Inventory.Add(gameObject.name);
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag.Equals("Player"))
		{
			playerIn = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag.Equals("Player"))
		{
			playerIn = false;
		}
	}
}
