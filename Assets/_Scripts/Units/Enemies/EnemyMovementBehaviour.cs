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
