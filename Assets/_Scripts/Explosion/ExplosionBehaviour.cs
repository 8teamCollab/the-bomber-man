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


public class ExplosionBehaviour : MonoBehaviour
{
    #region Variables

    [SerializeField] private float explosionDuration = 1f;

    #endregion Variables


    private void Start()
    {
        Invoke("BoxColliderSetNotActive", 0.1f);
        Destroy(gameObject, explosionDuration);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            other.GetComponent<BombBehaviour>().Explode();
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLogicBehaviour>().LoseLife(PlayerLogicBehaviour.Instance.ExplosionDamage);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyLogicBehaviour>().LoseLife(PlayerLogicBehaviour.Instance.ExplosionDamage);
        }
        else if (other.CompareTag("Powerup"))
        {
            other.gameObject.SetActive(false);
        }
    }


    private void BoxColliderSetNotActive() => GetComponent<BoxCollider2D>().enabled = false;
}
