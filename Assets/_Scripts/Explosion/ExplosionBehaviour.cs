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
