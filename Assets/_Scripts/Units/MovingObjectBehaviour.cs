using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;


public abstract class MovingObjectBehaviour : MonoBehaviour
{
    #region Variables

    [SerializeField] private LayerMask blockingLayer;

    [Header("Tile references")]
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase boxTile;

    private float moveTime = 0.1f;
    private float inverseMoveTime;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private Tilemap tilemapGameplay;

    #endregion Variables


    protected virtual void Start()
    {
        inverseMoveTime = 1f / moveTime;

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        tilemapGameplay = GameObject.Find("/Grid/TilemapGameplay").GetComponent<Tilemap>();
    }


    private IEnumerator SmoothMovement(Vector3 endPosition)
    {
        float sqrRemainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, endPosition, inverseMoveTime * Time.deltaTime);

            rigidbody.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - endPosition).sqrMagnitude;

            yield return null;
        }
    }


    private bool Move(Vector2 movementDirection, out RaycastHit2D raycastHit)
    {
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + movementDirection;

        TransformRaycastVectors(movementDirection, ref startPosition, ref endPosition);

        raycastHit = Physics2D.Linecast(startPosition, endPosition, blockingLayer);

        if (!raycastHit.transform)
        {
            StartCoroutine(SmoothMovement(endPosition));
            return true;
        }

        return false;
    }


    private void TransformRaycastVectors(Vector2 movementDirection, ref Vector2 start, ref Vector2 end)
    {
        float offset = 0.5f;

        float xDirection = movementDirection.x;
        float yDirection = movementDirection.y;

        if ((xDirection != 0) && (yDirection == 0))
        {
            if (xDirection == 1)
            {
                start += new Vector2(xDirection - offset, yDirection);
                end = start + new Vector2(xDirection - offset, yDirection);
            }
            else if (xDirection == -1)
            {
                start += new Vector2(xDirection + offset, yDirection);
                end = start + new Vector2(xDirection + offset, yDirection);
            }
        }
        else if ((xDirection == 0) && (yDirection != 0))
        {
            if (yDirection == 1)
            {
                start += new Vector2(xDirection, yDirection - offset);
                end = start + new Vector2(xDirection, yDirection - offset);
            }
            else if (yDirection == -1)
            {
                start += new Vector2(xDirection, yDirection + offset);
                end = start + new Vector2(xDirection, yDirection + offset);
            }
        }
    }


    protected virtual void AttemptMove<T>(Vector2 movementDirection)
        where T : Component
    {
        bool canMove = Move(movementDirection, out RaycastHit2D raycastHit);

        if (!raycastHit.transform)
        {
            return;
        }

        T raycastHitComponent = raycastHit.transform.GetComponent<T>();

        if (raycastHitComponent && !canMove)
        {
            OnCantMove(raycastHitComponent);
        }
    }
    protected virtual void AttemptMove(Vector2 movementDirection)
    {
        Move(movementDirection, out RaycastHit2D raycastHit);

        if (!raycastHit.transform)
        {
            return;
        }
    }


    protected virtual void ExecuteMove<T>(Vector2Int movementDirection)
        where T : Component
    {
        CheckTilesAround(transform.position, ref movementDirection);

        if (movementDirection.x != 0)
        {
            movementDirection.y = 0;
        }

        if ((movementDirection.x != 0) || (movementDirection.y != 0))
        {
            SetAnimatorDirectionValue(movementDirection);
            AttemptMove<T>(movementDirection);
        }
    }
    protected virtual void ExecuteMove(Vector2Int movementDirection)
    {
        CheckTilesAround(transform.position, ref movementDirection);

        if (movementDirection.x != 0)
        {
            movementDirection.y = 0;
        }

        if ((movementDirection.x != 0) || (movementDirection.y != 0))
        {
            SetAnimatorDirectionValue(movementDirection);
            AttemptMove(movementDirection);
        }
    }


    private void CheckTilesAround(Vector3 movingObjectPosition, ref Vector2Int movementDirection)
    {
        TileBase leftTile  = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(movingObjectPosition + Vector3.left));
        TileBase rightTile = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(movingObjectPosition + Vector3.right));
        TileBase upperTile = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(movingObjectPosition + Vector3.up));
        TileBase lowerTile = tilemapGameplay.GetTile(tilemapGameplay.WorldToCell(movingObjectPosition + Vector3.down));

        bool leftTileIsNotWalkable  = leftTile  == wallTile || leftTile == boxTile;
        bool rightTileIsNotWalkable = rightTile == wallTile || rightTile == boxTile;
        bool upperTileIsNotWalkable = upperTile == wallTile || upperTile == boxTile;
        bool lowerTileIsNotWalkable = lowerTile == wallTile || lowerTile == boxTile;

        if ((leftTileIsNotWalkable  && (movementDirection.x < 0)) ||
            (rightTileIsNotWalkable && (movementDirection.x > 0)))
        {
            movementDirection.x = 0;
        }

        if ((upperTileIsNotWalkable && (movementDirection.y > 0)) ||
            (lowerTileIsNotWalkable && (movementDirection.y < 0)))
        {
            movementDirection.y = 0;
        }
    }


    private void SetAnimatorDirectionValue(Vector2Int movementDirection)
    {
        if (movementDirection.x < 0)
        {
            animator.SetInteger("xDirection", -1);
        }
        else if (movementDirection.x > 0)
        {
            animator.SetInteger("xDirection", 1);
        }
        else
        {
            animator.SetInteger("xDirection", 0);
        }


        if (movementDirection.y < 0)
        {
            animator.SetInteger("yDirection", -1);
        }
        else if (movementDirection.y > 0)
        {
            animator.SetInteger("yDirection", 1);
        }
        else
        {
            animator.SetInteger("yDirection", 0);
        }
    }


    protected abstract void OnCantMove<T>(T component)
        where T : Component;


    protected abstract IEnumerator WaitTurnDelay();
}
