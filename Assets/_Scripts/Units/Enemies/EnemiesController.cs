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
