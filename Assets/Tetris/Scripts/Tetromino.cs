using UnityEngine;
using System.Collections;

public class Tetromino : MonoBehaviour {
    // Time since last gravity tick
    protected float lastFall = 0;
    float lockDelay = 1.0f;
    [SerializeField] private AudioClip spawnSound;

    // Use this for initialization
    void Start () {
        AudioSource.PlayClipAtPoint(spawnSound, Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
        if (Spawner.isLocking) return;
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft();
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            rotateLeft();
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            rotateRight();
        }

        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            softDrop();
        }

        // Fall
        else if (Time.time - lastFall >= 1)
        {
            moveDown();

            lastFall = Time.time;
        }

    }

    public void moveLeft()
    {
        Vector3 nextMovement = new Vector3(-1, 0, 0);

        // See if valid
        if (isValidGridPos(nextMovement))
        {
            // Modify position
            transform.position += nextMovement;
            // It's valid. Update grid.
            updateGrid();
        }
    }

    public void moveRight()
    {
        Vector3 nextMovement = new Vector3(1, 0, 0);

        // See if valid
        if (isValidGridPos(nextMovement))
        {
            // Modify position
            transform.position += nextMovement;
            // It's valid. Update grid.
            updateGrid();
        }
    }

    public void moveDown()
    {
        // Modify position
        Vector3 positionChange = new Vector3(0, -1, 0);

        // See if valid
        if (isValidGridPos(positionChange))
        {
            transform.position += positionChange;
            // It's valid. Update grid.
            updateGrid();
        }
        else
        {
            lockPiece();
        }
    }

    void lockPiece()
    {
        Spawner.isLocking = true;
        // Disable script
        enabled = false;

        //Invoke("LockPieceHelper", 1.0f);
        StartCoroutine(LockPieceHelper());

    }

    IEnumerator LockPieceHelper()
    {
        if (Grid.isFullRows())
        {
            yield return new WaitForSeconds(1.0f);
            // Clear filled horizontal lines
            Grid.deleteFullRows();
        }

        Spawner.isLocking = false;

        // Spawn next Group
        FindObjectOfType<Spawner>().spawnNext();
    }

    void softDrop()
    {
        Vector3 dropDistance = new Vector3(0, 1, 0);
        if (!isValidGridPos(dropDistance))
        {
            return;
        }
        
        while (isValidGridPos(dropDistance))
        {
            dropDistance.y++;
        }

        transform.position += dropDistance;

    }

    protected bool isValidGridPos(Vector3 change)
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position + change);

            // Not inside Border?
            if (!Grid.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    protected bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            // Not inside Border?
            if (!Grid.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    public void rotateLeft()
    {
        transform.Rotate(0, 0, 90);

        // See if valid
        if (isValidGridPos())
        {
            // It's valid. Update grid.
            updateGrid();
        }
        else
        {
            // It's not valid. revert.
            transform.Rotate(0, 0, -90);
        }
    }

    public void rotateRight()
    {
        transform.Rotate(0, 0, -90);

        // See if valid
        if (isValidGridPos())
        {
            // It's valid. Update grid.
            updateGrid();
        }
        else
        {
            // It's not valid. revert.
            transform.Rotate(0, 0, 90);

            // wall kick
            
        }
    }

    protected void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
