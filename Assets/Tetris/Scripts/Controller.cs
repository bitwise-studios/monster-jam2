using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Tetromino currentPiece = GameObject.Find("Spawner").GetComponent<Spawner>()
            .getCurrentTetromino().GetComponent<Tetromino>();
        if (Input.GetKey(KeyCode.DownArrow))
            currentPiece.moveDown();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentPiece.moveRight();
        }
    }
}
