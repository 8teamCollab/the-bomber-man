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
