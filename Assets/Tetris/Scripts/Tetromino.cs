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
        /* TODO: replace with AI code */
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
        // Clear filled horizontal lines
        Grid.deleteFullRows();

        // Spawn next Group
        FindObjectOfType<Spawner>().spawnNext();

        // Disable script
        enabled = false;
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

    virtual protected bool rotateLeft()
    {
        transform.Rotate(0, 0, 90);

        // See if valid
        if (isValidGridPos())
        {
            // It's valid. Update grid.
            updateGrid();
            return true;
        }
        else
        {
            // It's not valid. revert.
            transform.Rotate(0, 0, -90);
            return false;
        }
    }

    virtual protected bool rotateRight()
    {
        transform.Rotate(0, 0, -90);

        // See if valid
        if (isValidGridPos())
        {
            // It's valid. Update grid.
            updateGrid();
            return true;
        }
        else
        {
            // It's not valid. revert.
            transform.Rotate(0, 0, 90);

            // wall kick
            
            return false;
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

	public AIPiece ToAIPiece() {
		int starty = 999, startx = 999;
		int endx = -1, endy = -1;
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
			if ((int)v.x < startx) startx = (int)v.x;
			if ((int)v.y < starty) starty = (int)v.y;
			if ((int)v.x > endx) endx = (int)v.x;
			if ((int)v.y > endy) endy = (int)v.y;
        }
		int[,] outmino = new int[endy + 1 - starty, endx + 1 - startx];
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
			outmino[((int)v.y) - starty, ((int)v.x) - startx] = 1;
        }
		return new AIPiece(startx, starty, outmino);
	}

}
