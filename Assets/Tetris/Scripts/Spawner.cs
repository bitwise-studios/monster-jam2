using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [SerializeField] private GameObject[] groups;
    private GameObject currentTetromino;

    public static bool isLocking = false;

    // these are static because y0l0
    private static int lastClear = 0; // how many blocks ago was the last clear?
    private static long rageLevel = 100;
    private static long score = 0;
    private static bool gameOver = false;

	// Use this for initialization
	void Start () {
        spawnNext();
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}

    public static long getScore()
    {
        return score;
    }

    public static void lineClearNotify(int lines)
    {
        lastClear = 0;
        rageLevel /= (long) Mathf.Pow(2, lines);
    }

    public static void blockLockNotify()
    {
        lastClear++;
        if (lastClear != 0)
        {
            if (lastClear % 5 == 0)
            {
                // update rage level
                rageLevel = (long)(rageLevel * 2);
                rageLevel++;
            }
        }
        if(rageLevel > 100)
        {
            score += (long) rageLevel / 10;
        }
        else
        {
            score += (rageLevel - 100) * 10;
        }
    }

    public static long getRageLevel()
    {
        return rageLevel;
    }

    public static bool isGameOver()
    {
        return gameOver;
    }

    void resetGame()
    {
        // need to clear grid
        Grid.clearAllRows();
        GetComponentInChildren<AudioSource>().Play();
        Tetromino[] tetrominos = FindObjectsOfType<Tetromino>();
        foreach(Tetromino t in tetrominos)
        {
            Destroy(t);
        }
        isLocking = false;
        Start();
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
        if (isLocking) return;
        if (isDeadCheck())
        {
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            Tetromino[] allTetrominoes = FindObjectsOfType<Tetromino>();

            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }

            foreach(Tetromino t in allTetrominoes)
            {
                t.enabled = false;
            }

            print("Game over!");
            isLocking = true;
            if(rageLevel == 0)
            {
                rageLevel = 10;
            }
            else
                rageLevel *= 10;

            if (rageLevel <= 10000)
                Invoke("resetGame", 1.0f);
            else
                gameOver = true;
            return;
        }

        // random index
        int i = Random.Range(0, groups.Length);

        // Spawn group at current position
        currentTetromino = (GameObject) Instantiate(groups[i], transform.position, Quaternion.identity);

    }

    public GameObject getCurrentTetromino()
    {
        return currentTetromino;
    }
}
