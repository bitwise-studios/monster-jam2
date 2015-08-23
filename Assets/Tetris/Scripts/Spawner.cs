using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [SerializeField] private GameObject[] groups;

	// Use this for initialization
	void Start () {
        spawnNext();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void spawnNext()
    {
        if (Grid.grid[(int)transform.position.x, (int)transform.position.y] != null)
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
        Instantiate(groups[i], transform.position, Quaternion.identity);

    }
}
