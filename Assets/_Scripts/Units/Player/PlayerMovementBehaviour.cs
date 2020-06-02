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
using UnityStandardAssets.CrossPlatformInput;


public class PlayerMovementBehaviour : MovingObjectBehaviour
{
    #region Variables

    [HideInInspector] public string horizontalAxisKey = "Horizontal";
    [HideInInspector] public string verticalAxisKey = "Vertical";

    private Vector2Int movementDirection = Vector2Int.zero;
    private bool ourTurn = true;

    #endregion Variables


    private void Update()
    {
        if (!ourTurn)
        {
            return;
        }

        movementDirection.x = (int)CrossPlatformInputManager.GetAxisRaw(horizontalAxisKey);
        movementDirection.y = (int)CrossPlatformInputManager.GetAxisRaw(verticalAxisKey);

        ExecuteMove(movementDirection);
    }


    protected override void AttemptMove(Vector2 movementDirection)
    {
        base.AttemptMove(movementDirection);

        ourTurn = false;
        StartCoroutine(WaitTurnDelay());
    }


    protected override void ExecuteMove(Vector2Int movementDirection) => base.ExecuteMove(movementDirection);


    protected override void OnCantMove<T>(T component) { }


    protected override IEnumerator WaitTurnDelay()
    {
        float speed = GetComponent<PlayerLogicBehaviour>().Speed;
        yield return new WaitForSeconds(0.1f * speed);
        ourTurn = true;
    }
}
