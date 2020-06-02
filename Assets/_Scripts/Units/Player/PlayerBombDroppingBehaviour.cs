/*
 * Copyright(C) 2020 Artyom Bezmenov (8nhuman8)

 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * at your option) any later version.

 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see<https://www.gnu.org/licenses/>.
 */


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
