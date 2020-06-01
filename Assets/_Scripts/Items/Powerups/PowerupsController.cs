using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PowerupsController : SingletonBehaviour<PowerupsController>
{
    #region Variables

    [SerializeField] private float spawnChance = 0.5f;

    [Header("Objects references")]
    [SerializeField] private TileBase box;
    [SerializeField] private GameObject powerupGameObject;

    [Header("Powerups")]
    [SerializeField] private List<Powerup> powerups;

    private Dictionary<Powerup, float> activePowerups = new Dictionary<Powerup, float>();
    private List<Powerup> keys = new List<Powerup>();

    #endregion Variables


    private void Update() => HandleActivePowerups();


    private void HandleActivePowerups()
    {
        bool changed = false;

        if (activePowerups.Count > 0)
        {
            foreach (Powerup powerup in keys)
            {
                if (activePowerups[powerup] > 0)
                {
                    activePowerups[powerup] -= Time.deltaTime;
                }
                else
                {
                    changed = true;

                    activePowerups.Remove(powerup);

                    powerup.EndAction();
                }
            }
        }

        if (changed)
        {
            keys = new List<Powerup>(activePowerups.Keys);
        }
    }


    public void ActivatePowerup(Powerup powerup)
    {
        if (!activePowerups.ContainsKey(powerup))
        {
            powerup.StartAction();
            activePowerups.Add(powerup, powerup.duration);
        }
        else
        {
            activePowerups[powerup] += powerup.duration;
        }

        keys = new List<Powerup>(activePowerups.Keys);
    }


    public void SpawnPowerups()
    {
        float offset = 0.5f;
        Tilemap tilemapGameplay = GameObject.Find("/Grid/TilemapGameplay").GetComponent<Tilemap>();
        Transform powerupsHolder = new GameObject("Powerups").transform;

        for (int x = 0; x < MapManager.Instance.width; x++)
        {
            for (int y = 0; y < MapManager.Instance.height; y++)
            {
                Vector3 spawnPosition = new Vector3(x + offset, y + offset);
                Vector3Int spawnCellPosition = tilemapGameplay.WorldToCell(spawnPosition);
                TileBase tile = tilemapGameplay.GetTile(spawnCellPosition);

                bool spawning = Random.value > (1f - spawnChance);

                if ((tile == box) && spawning)
                {
                    int randomIndex = Random.Range(0, powerups.Count);
                    Powerup randomPowerup = powerups[randomIndex];

                    GameObject powerupInstance = Instantiate(powerupGameObject, spawnPosition, Quaternion.identity, powerupsHolder) as GameObject;

                    PowerupMainBehaviour powerupMainBehaviour = powerupInstance.GetComponent<PowerupMainBehaviour>();
                    powerupMainBehaviour.powerupsController = this;
                    powerupMainBehaviour.SetPowerup(randomPowerup);
                }
            }
        }
    }
}
