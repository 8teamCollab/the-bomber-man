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


public class PowerupLogicBehaviour : MonoBehaviour
{
    #region Variables

    [SerializeField] private float lifetime = 15f;

    private Tilemap tilemapGameplay;
    private BoxCollider2D boxCollider;
    private PowerupMainBehaviour powerup;

    private bool calledOnce = false;

    #endregion Variables


    private void Start()
    {
        tilemapGameplay = GameObject.Find("/Grid/TilemapGameplay").GetComponent<Tilemap>();

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        powerup = GetComponent<PowerupMainBehaviour>();
    }


    private void Update()
    {
        if (!calledOnce && !InTheBox())
        {
            Invoke("DestroyByExpirationOfLifetime", lifetime);
            Invoke("BoxColliderSetActive", 0.1f);

            calledOnce = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayPowerupSound();
            powerup.ActivatePowerup();
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }


    private void PlayPowerupSound()
    {
        switch (powerup.type)
        {
            case PowerupType.Negative:
                AudioManager.Instance.PlaySound(Sound.NegativePowerup);
                break;
            case PowerupType.Positive:
                AudioManager.Instance.PlaySound(Sound.PositivePowerup);
                break;
            default:
                Debug.LogError($"Powerup {name} has a non-existent PowerupType: {powerup.type}");
                break;
        }
    }


    private bool InTheBox()
    {
        Vector3 tilePosition = tilemapGameplay.WorldToLocal(transform.position);
        Vector3Int tileCellPosition = tilemapGameplay.WorldToCell(tilePosition);
        TileBase tile = tilemapGameplay.GetTile(tileCellPosition);

        return tile != null;
    }


    private void DestroyByExpirationOfLifetime() => gameObject.SetActive(false);


    private void BoxColliderSetActive() => boxCollider.enabled = true;
}
