using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Tetromino : MonoBehaviour {

    float fall = 0;
    public float fallSpeed = 1;

    public bool allowRotation = true;
    public bool limitRotation = false;

    public Game game;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if  (Input.GetButtonDown("MoveRight"))
        {
            MoveRight();
        }
        if (Input.GetButtonDown("MoveLeft"))
        {
            MoveLeft();
        }
        if (Input.GetButtonDown("Rotate"))
        {
            Rotate();
        }
        if (Input.GetButton("MoveDown")|| Time.time - fall >= fallSpeed)
        {
            MoveDown();
        }
    }

    public void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);
        if (CheckIsValidPosition())
        {
            UpdateGrid();
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    public void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
        if (CheckIsValidPosition())
        {
            UpdateGrid();
        }
        else
        {
            transform.position += new Vector3(1, 0, 0);
        }

    }

    public void Rotate()
    {
        //Allow rotation?
        if (allowRotation)
        {
            //Tetromino Type?
            if (limitRotation)
            {
                //Limited : angle>=90 ->rotate it backwards
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
            //UnLimited: Rotate it round and round!
            else
            {
                transform.Rotate(0, 0, 90);
            }

            //UnValid:bomp the wall->action backwards
            if (CheckIsValidPosition())
            {
                UpdateGrid();
            }
            else
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }

                }
                else
                {
                    transform.Rotate(0, 0, -90);//Rotation forbidden (UnValid)
                }
            }
        }
    }

    public void MoveDown()
    {
        transform.position += new Vector3(0, -1, 0);
        if (CheckIsValidPosition())
        {
            UpdateGrid();
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            game.DeleteRow();
            if (CheckOvergridHeight())
            {
                Game.GameOver();
            }
            //SpawnNextOne
            enabled = false;
            game.SpawnNextTetromino();
        }

        fall = Time.time;
    }

    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2Int pos =Game.Round(mino.position);
            if (Game.CheckIsInsideGrid(pos) == false)
            {
                return false;
            }
           
            if (Game.grid[pos.x, pos.y] != null && Game.grid[pos.x, pos.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }


    public void UpdateGrid()
    {
        for (int y = 0; y < Game.gridHeight; ++y)
        {
            for (int x = 0; x < Game.gridWidth; ++x)
            {
                if (Game.grid[x, y] != null)
                {
                    if (Game.grid[x, y].parent == transform)
                    {
                        Game.grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform mino in transform)
        {
            Vector2Int pos = Game.Round(mino.position);
            if (pos.y < Game.gridHeight)
            {
                Game.grid[pos.x, pos.y] = mino;
            }
        }
    }

    public bool CheckOvergridHeight()
    {
        for (int x = 0; x < Game.gridWidth; ++x)
        {
            foreach (Transform mino in transform)
            {
                Vector2Int pos = Game.Round(mino.position); 
                if (pos.y > Game.gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }


}
