using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataDisplayController : MonoBehaviour {

    [SerializeField] private Text rageLevelDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rageLevelDisplay.text = "Rage Level: " + Spawner.getRageLevel();
	}
}
