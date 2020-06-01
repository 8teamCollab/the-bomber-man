using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapManager : SingletonBehaviour<MapManager>
{
    #region Variables

    [Header("Map size")]
    [Range(5f, 20f)] public int width = 15;
    [Range(5f, 20f)] public int height = 11;


    [Header("Game object references")]
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject exitGameObject;

    [Header("Game object count")]
    [SerializeField] private Count boxCount = new Count(7, 7);

    [Header("Tile references")]
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase boxTile;

    [Header("Floor tiles references")]
    [SerializeField] private TileBase floorDL;
    [SerializeField] private TileBase floorEmpty;
    [SerializeField] private TileBase floorRD;
    [SerializeField] private TileBase floorRDL;
    [SerializeField] private TileBase floorRL;
    [SerializeField] private TileBase floorUD;
    [SerializeField] private TileBase floorUDL;
    [SerializeField] private TileBase floorUL;
    [SerializeField] private TileBase floorUR;
    [SerializeField] private TileBase floorURD;
    [SerializeField] private TileBase floorURDL;
    [SerializeField] private TileBase floorURL;

    private Tilemap tilemapFloor;
    private Tilemap tilemapGameplay;

    private List<Vector3Int> grid = new List<Vector3Int>();

    #endregion Variables


    [System.Serializable]
    public class Count
    {
        public int min;
        public int max;

        public Count(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }


    private void InitialiseGrid()
    {
        grid.Clear();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool isPositionOfWall = (x % 2 != 0) && (y % 2 != 0);
                bool isPositionOfSpawn = ((x == 0) && (y == height - 1)) ||
                                         ((x == 0) && (y == height - 2)) ||
                                         ((x == 1) && (y == height - 1));
                bool isPositionOfExit = (x == width - 1) && (y == 0);

                if (isPositionOfWall || isPositionOfSpawn || isPositionOfExit)
                {
                    continue;
                }

                grid.Add(new Vector3Int(x, y, 0));
            }
        }
    }


    private void InitialiseTilemaps()
    {
        tilemapFloor = GameObject.Find("/Grid/TilemapFloor").GetComponent<Tilemap>();
        tilemapGameplay = GameObject.Find("/Grid/TilemapGameplay").GetComponent<Tilemap>();

        tilemapFloor.ClearAllTiles();
        tilemapGameplay.ClearAllTiles();
    }


    private void MapSetup()
    {
        for (int x = -1; x <= width; x++)
        {
            for (int y = -1; y <= height; y++)
            {
                TileBase tileToInstantiate = SetTile(x, y);

                if (tileToInstantiate == wallTile)
                {
                    tilemapFloor.SetTile(new Vector3Int(x, y, 0), floorEmpty);
                    tilemapGameplay.SetTile(new Vector3Int(x, y, 0), tileToInstantiate);
                }
                else
                {
                    tilemapFloor.SetTile(new Vector3Int(x, y, 0), tileToInstantiate);
                }
            }
        }
    }


    private TileBase SetTile(int x, int y)
    {
        if ((y == 0) && (x % 2 == 0) && (x != 0) && (x != width - 1))
        {
            return floorURL;
        }
        else if ((y == height - 1) && (x % 2 == 0) && (x != 0) && (x != width - 1))
        {
            return floorRDL;
        }
        else if ((x == width - 1) && (y % 2 == 0) && (y != 0) && (y != height - 1))
        {
            return floorUDL;
        }
        else if ((x == 0) && (y % 2 == 0) && (y != 0) && (y != height - 1))
        {
            return floorURD;
        }
        else if ((x == width - 1) && (y == 0))
        {
            return floorUL;
        }
        else if ((x == width - 1) && (y == height - 1))
        {
            return floorDL;
        }
        else if ((x == 0) && (y == height - 1))
        {
            return floorRD;
        }
        else if ((x == 0) && (y == 0))
        {
            return floorUR;
        }
        else if ((x % 2 == 0) && (y % 2 == 0))
        {
            return floorURDL;
        }
        else if ((x % 2 == 0) && (y % 2 != 0) && (x >= 0) && (y >= 0) && (y != height))
        {
            return floorUD;
        }
        else if ((x % 2 != 0) && (y % 2 == 0) && (x >= 0) && (y >= 0) && (x != width))
        {
            return floorRL;
        }
        else if ((x == -1) || (x == width) || (y == -1) || (y == height) ||
                 ((x % 2 != 0) && (x != -1) && (x != width) && (y % 2 != 0) && (y != -1) && (y != height)))
        {
            return wallTile;
        }
        else
        {
            return null;
        }
    }


    public Vector3Int RandomCellPosition()
    {
        int randomIndex = Random.Range(0, grid.Count);
        Vector3Int randomCellPosition = grid[randomIndex];
        grid.RemoveAt(randomIndex);
        return randomCellPosition;
    }


    private void LayoutObjectAtRandomPosition(TileBase tile, int min, int max)
    {
        int objectCount = Random.Range(min, max + 1);

        for (int i = 0; i < objectCount; i++)
        {
            tilemapGameplay.SetTile(RandomCellPosition(), tile);
        }
    }


    public void SetupScene(int level)
    {
        InitialiseGrid();
        InitialiseTilemaps();
        MapSetup();

        LayoutObjectAtRandomPosition(boxTile, boxCount.min, boxCount.max);

        NodeMapManager.Instance.NodeMapSetup();
        NodeMapManager.Instance.NodeMapStatesSetup();

        Vector3 exitSpawnPosition = tilemapFloor.GetCellCenterWorld(new Vector3Int(width - 1, 0, 0));
        Instantiate(exitGameObject, exitSpawnPosition, Quaternion.identity);

        Vector3 playerSpawnPosition = tilemapFloor.GetCellCenterWorld(new Vector3Int(0, height - 1, 0));
        Instantiate(playerGameObject, playerSpawnPosition, Quaternion.identity);

        EnemiesController.Instance.SpawnEnemies(level);
        PowerupsController.Instance.SpawnPowerups();
    }
}
