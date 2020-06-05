using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCellSystem : MonoBehaviour
{

    public Vector3 WorldOrigin;
    public float CellSize;

    public int WorldSizeCells = 10;
    public static WorldCellSystem Instance;

    private Cell[,] WorldGrid = new Cell[10, 10];
    private DebugCube[,] CubesList = new DebugCube[10, 10];

    private Vector2Int[] cellShifts = new Vector2Int[8];

    public GameObject CellPrefab;
    public GameObject GridParent;

    public Color NormalColor = Color.green;
    public Color FireColor = Color.red;

    bool isGridInitialized = false;

    float lastFireIncrease = 0f;
    float fireIncreaseTime = 1f;
   
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cellShifts[0] = new Vector2Int(0, 1);
        cellShifts[1] = new Vector2Int(1, 1);
        cellShifts[2] = new Vector2Int(1, 0);
        cellShifts[3] = new Vector2Int(1,-1);
        cellShifts[4] = new Vector2Int(0,-1);
        cellShifts[5] = new Vector2Int(-1,-1);
        cellShifts[6] = new Vector2Int(-1, 0);
        cellShifts[7] = new Vector2Int(-1,-1);

        GenerateGrid();
        CreateDebugCubes();
        isGridInitialized = true;

        AddFireSource(3);
    }

    void GenerateGrid()
    {
        WorldGrid = new Cell[WorldSizeCells, WorldSizeCells];

        int gridCount = 0;
        Vector3 currentPos = Vector3.zero;

        for(int x=0;x<WorldSizeCells;x++)
        {
            for (int z = 0; z < WorldSizeCells; z++)
            {
                Cell newCell = new Cell(x, z,currentPos);
                newCell.State = CellState.Normal;

                WorldGrid[x, z] = newCell;
                gridCount++;
                currentPos.z += CellSize;
            }
            // currentPos.x = WorldOrigin.x;
            currentPos.z = WorldOrigin.z;
            currentPos.x += CellSize;
        }

        Debug.Log("Cells created:" + gridCount);
    }

    //0 min, 10 - instant remove fire
    public void ApplyWater(Vector3 worldPos,float amount)
    {
        //get cell
        Cell c = GetCell(worldPos);

        Debug.Log(c.PositionIndex);
    
        c.FireDangerScale = 0;
    }

    public Cell GetCell(Vector3 position)
    {
        Cell maxCell = WorldGrid[WorldSizeCells-1, WorldSizeCells-1];
        
        Vector3 maxPosition = maxCell.CellWorldPos;
        maxPosition += new Vector3(CellSize, 0f, CellSize);
        Vector3 gridPosition = WorldCordsToCellCords(position);

        float cellIndexX = (gridPosition.x / maxPosition.x)* WorldSizeCells;
        float cellIndexY = (gridPosition.z / maxPosition.z)* WorldSizeCells;

        Cell resultCell = GetCell((int)cellIndexX, (int)cellIndexY);

        return resultCell;
    }

    Vector3 WorldCordsToCellCords(Vector3 worldPos)
    {
        Vector3 gridPos = worldPos - WorldOrigin;
        return gridPos;
    }

    Vector3 CellCordsToWorldCords(Vector3 cellCords)
    {
        Vector3 resultPos = cellCords + WorldOrigin;
        return resultPos;
    }

    public void SimulateFire()
    {
        foreach (Cell c in WorldGrid)
        {
            c.FireDangerScale = Random.Range(0, 4);
        }
    }

    public void UpdateCellsState()
    {
        if (Time.time - lastFireIncrease > fireIncreaseTime)
        {
            lastFireIncrease = Time.time;
            foreach (Cell c in WorldGrid)
            {
                if (c.FireDangerScale >= 3)
                {
                    c.FireDangerScale += 1;
                }

                //10 Fire rating can flame near cells
                if (c.FireDangerScale >= 10)
                {
                    //check neighbor cells
                    for (int i = 0; i < 8; i++)
                    {
                        var nearCell = GetNearCell(c, i);
                        if (nearCell != null)
                        {
                            nearCell.FireDangerScale += 1;
                        }
                    }
                }
            }

         
            Debug.Log("Fire expanded!");
        }
    }

    Cell GetNearCell(Cell c, int index)
    {
        //clockwise indexes
        Vector2Int shift = cellShifts[index];
        Vector2Int newCord = c.PositionIndex + shift;
        Cell nextCell= GetCell(newCord.x, newCord.y);

        return nextCell;
    }

    public void CreateDebugCubes()
    {
        CubesList = new DebugCube[WorldSizeCells, WorldSizeCells];

        foreach(Cell c in WorldGrid)
        {
            Vector3 pos = CellCordsToWorldCords(c.CellWorldPos);
            GameObject cellObj = Instantiate(CellPrefab, pos, Quaternion.identity);
            cellObj.transform.SetParent(GridParent.transform);
            CubesList[c.PositionIndex.x, c.PositionIndex.y] = cellObj.GetComponent<DebugCube>();
        }
    }

    void UpdateCubesVisual()
    {
        foreach(Cell c in WorldGrid)
        {
            DebugCube cube= CubesList[c.PositionIndex.x, c.PositionIndex.y];

            if (cube != null)
            {
                float danger = (float) c.FireDangerScale / (float) 10;

                Color tempColor = Color.Lerp(NormalColor, FireColor,danger);
                cube.SetCubeColor(tempColor);

                //if (c.State == CellState.Normal)
                //{
                //    cube.SetCubeColor(NormalColor);
                //}
                //else if (c.State == CellState.Burning)
                //{
                //    cube.SetCubeColor(FireColor);
                //}
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGridInitialized)
        {
            UpdateCellsState();
            UpdateCubesVisual();
        }
    }


    public Cell GetCell(int x,int y)
    {
        x = Mathf.Clamp(x, 0, WorldSizeCells-1);
        y = Mathf.Clamp(y, 0, WorldSizeCells-1);

        return WorldGrid[x, y];
    }

    public void AddFireSource(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomX = Random.Range(0, WorldSizeCells);
            int randomY = Random.Range(0, WorldSizeCells);

            var cell = GetCell(randomX, randomY);
            //start with medium fire
            cell.FireDangerScale = 5;
        }

    }

    public class Cell
    {
        public Vector2Int PositionIndex;
        public Vector3 CellWorldPos;
        public CellState State=CellState.Normal;
        public int FireDangerScale = 0;
        public float LastFireUpdate = 0f;

        public Cell(int x,int y,Vector3 worldPos)
        {
            PositionIndex = new Vector2Int(x, y);
            CellWorldPos = worldPos;
        }
    }

    public enum CellState
    {
        Normal,
        Burning,
    }
}
