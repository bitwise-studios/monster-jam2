﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [SerializeField] private GameObject[] groups;
    private GameObject currentTetromino;
	private AI ai = new AI(0.510066, 0.760666, 0.35663, 0.184483);

	// Use this for initialization
	void Start () {
        spawnNext();
	}
	
	// Update is called once per frame
	void Update () {

	}

    bool isDeadCheck()
    {
        for(int i = -5; i <= 1; i++)
        {
            for(int ii = -1; i <= 1; i++)
            {
                if (Grid.grid[(int)transform.position.x + i, (int)transform.position.y + ii] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void spawnNext()
    {
        if (isDeadCheck())
        {
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }

            print("Game over!");
            return;
        }

        // random index
        int i = Random.Range(0, groups.Length);

        // Spawn group at current position
        currentTetromino = (GameObject) Instantiate(groups[i], transform.position, Quaternion.identity);
		AI.Result res = ai.Best(Grid.ToSimpleGrid(), currentTetromino.GetComponent<Tetromino>());
    }

    public GameObject getCurrentTetromino()
    {
        return currentTetromino;
    }
}
