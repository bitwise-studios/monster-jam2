using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataDisplayController : MonoBehaviour {

    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text rageLevelDisplay;
    [SerializeField] private Text gameStateDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        scoreDisplay.text = "Score: " + Spawner.getScore();
        rageLevelDisplay.text = "Rage Level: " + Spawner.getRageLevel();
        updateGameState();
	}

    void updateGameState()
    {
        if (Spawner.isGameOver())
            gameStateDisplay.text = "Game Over.";
        else if (Spawner.getRageLevel() > 1000)
        {
            gameStateDisplay.text = "WARNING! RAGEQUIT IMMINENT!";
        }
        else if (Spawner.getRageLevel() > 500)
        {
            gameStateDisplay.text = "Stressing";
        }
        else if (Spawner.getRageLevel() > 250)
        {
            gameStateDisplay.text = "Starting to get difficult...";
        }
        else if (Spawner.getRageLevel() >= 100)
        {
            gameStateDisplay.text = "Fair";
        }
        else
        {
            gameStateDisplay.text = "Too easy.";
        }
    }
}
