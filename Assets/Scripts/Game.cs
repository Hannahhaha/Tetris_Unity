using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public Tetromino tetromino;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    // Use this for initialization
    void Start() {
        SpawnNextTetromino();
    }

    public void SpawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 19.0f), Quaternion.identity);
        var tetroComp = nextTetromino.GetComponent<Tetromino>();
        tetroComp.game = this;
    }


    string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(1, 8);
        string randomTetrominoName = "Prefabs/Tetromino_T";
        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = "Prefabs/Tetromino_T";
                break;
            case 2:
                randomTetrominoName = "Prefabs/Tetromino_Long";
                break;
            case 3:
                randomTetrominoName = "Prefabs/Tetromino_Square";
                break;
            case 4:
                randomTetrominoName = "Prefabs/Tetromino_J";
                break;
            case 5:
                randomTetrominoName = "Prefabs/Tetromino_L";
                break;
            case 6:
                randomTetrominoName = "Prefabs/Tetromino_Z";
                break;
            case 7:
                randomTetrominoName = "Prefabs/Tetromino_S";
                break;

        }
        return randomTetrominoName;
    }


    public static bool CheckIsInsideGrid(Vector2Int pos)
    {
        return (pos.x >= 0 &&pos.x < gridWidth && pos.y >= 0);
    }

    /// <summary>
    /// 将显示位置转为逻辑坐标
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector2Int Round(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        return new Vector2Int(x, y);
    }

    public Transform GetTransformAtGridPosition(Vector2Int pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[pos.x, pos.y];
        }
    }

    public bool IsFullRow(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }


    public void DeleteMino(int y)
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if(grid[x, y]!= null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowDown(int y)
    {
        for(int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for(int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRow(y))
            {
                DeleteMino(y);
                MoveAllRowDown(y+1);
                --y;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }


}
