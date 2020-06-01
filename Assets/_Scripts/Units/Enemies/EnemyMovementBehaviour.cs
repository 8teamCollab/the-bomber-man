using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EnemyMovementBehaviour : MovingObjectBehaviour
{
    #region Variables

    private EnemyIntelligenceBehaviour enemyIntelligence;
    private EnemyLogicBehaviour enemyLogic;

    private Vector2Int movementDirection =  Vector2Int.zero;
    private bool ourTurn = true;

    private Tilemap tilemapGameplay;

    #endregion Variables


    private void Awake()
    {
        enemyIntelligence = GetComponent<EnemyIntelligenceBehaviour>();
        enemyLogic = GetComponent<EnemyLogicBehaviour>();

        tilemapGameplay = GameObject.Find("/Grid/TilemapGameplay").GetComponent<Tilemap>();
    }


    private void Update()
    {
        if (!ourTurn || IsStuck())
        {
            return;
        }

        movementDirection = Vector2Int.zero;

        if (PlayerLogicBehaviour.Instance && enemyIntelligence.PlayerInFieldOfView())
        {
            enemyIntelligence.DoAgressiveMovements(ref movementDirection);
        }
        else
        {
            enemyIntelligence.DoRandomMovements(ref movementDirection);
        }

        ExecuteMove<PlayerLogicBehaviour>(movementDirection);
    }


    protected override void AttemptMove<T>(Vector2 movementDirection)
    {
        base.AttemptMove<T>(movementDirection);

        ourTurn = false;
        StartCoroutine(WaitTurnDelay());
    }


    protected override void ExecuteMove<T>(Vector2Int movementDirection) => base.ExecuteMove<T>(movementDirection);


    protected override void OnCantMove<T>(T component)
    {
        PlayerLogicBehaviour player = component as PlayerLogicBehaviour;
        enemyLogic.HitPlayer(player);
    }


    protected override IEnumerator WaitTurnDelay()
    {
        float speed = GetComponent<EnemyLogicBehaviour>().Speed;
        yield return new WaitForSeconds(0.1f * speed);
        ourTurn = true;
    }


    private bool IsStuck()
    {
        TileBase upperTile = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(transform.position + Vector3.up));
        TileBase lowerTile = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(transform.position + Vector3.down));
        TileBase leftTile  = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(transform.position + Vector3.left));
        TileBase rightTile = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(transform.position + Vector3.right));

        return upperTile && lowerTile && leftTile && rightTile;
    }
}
