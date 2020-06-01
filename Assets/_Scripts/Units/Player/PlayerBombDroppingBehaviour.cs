using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerBombDroppingBehaviour : SingletonBehaviour<PlayerBombDroppingBehaviour>
{
    #region Variables

    [SerializeField] private GameObject bombGameObject;

    private Tilemap tilemapFloor;

    #endregion Variables


    private void Start() => tilemapFloor = GameObject.Find("/Grid/TilemapFloor").GetComponent<Tilemap>();


    public void DropBomb()
    {
        Vector3Int currentTileCellPosition = tilemapFloor.WorldToCell(transform.position);
        Vector3 currentTilePosition = tilemapFloor.GetCellCenterWorld(currentTileCellPosition);

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");

        if (!AllAvailableBombsAreSpawned(bombs) && !ThereABombOnTheSamePosition(bombs, currentTilePosition))
        {
            Instantiate(bombGameObject, currentTilePosition, Quaternion.identity);
        }
    }


    private bool AllAvailableBombsAreSpawned(GameObject[] bombs) => bombs.Length == PlayerLogicBehaviour.Instance.BombsCapacity;


    private bool ThereABombOnTheSamePosition(GameObject[] bombs, Vector3 position)
    {
        foreach (GameObject bomb in bombs)
        {
            if (bomb.transform.position == position)
            {
                return true;
            }
        }

        return false;
    }
}
