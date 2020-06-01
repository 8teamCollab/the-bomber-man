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
