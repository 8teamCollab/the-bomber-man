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


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EnemiesController : SingletonBehaviour<EnemiesController>
{
    #region Variables

    [SerializeField] private GameObject enemyGameObject;
    [SerializeField] private List<EnemyData> enemies;

    #endregion Variables


    public void SpawnEnemies(int level)
    {
        Tilemap tilemapFloor = GameObject.Find("/Grid/TilemapFloor").GetComponent<Tilemap>();
        Transform enemiesHolder = new GameObject("Enemies").transform;

        int enemySpawnCount = (int)Mathf.Log(level + 1, 2f);

        for (int i = 0; i < enemySpawnCount; i++)
        {
            int randomIndex = Random.Range(0, enemies.Count);
            EnemyData randomEnemyData = enemies[randomIndex];

            Vector3Int randomCellPosition = MapManager.Instance.RandomCellPosition();
            Vector3 spawnPosition = tilemapFloor.GetCellCenterWorld(randomCellPosition);
            GameObject enemyInstance = Instantiate(enemyGameObject, spawnPosition, Quaternion.identity, enemiesHolder) as GameObject;

            enemyInstance.GetComponent<EnemyLogicBehaviour>().SetEnemyData(randomEnemyData);
        }
    }
}
