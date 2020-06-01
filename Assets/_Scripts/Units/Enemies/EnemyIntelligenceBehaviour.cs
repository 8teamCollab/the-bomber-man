using System.Collections.Generic;
using UnityEngine;


public class EnemyIntelligenceBehaviour : MonoBehaviour
{
    #region Variables

    private EnemyLogicBehaviour enemyLogic;

    #endregion Variables


    private void Awake() => enemyLogic = GetComponent<EnemyLogicBehaviour>();


    public void DoRandomMovements(ref Vector2Int movementDirection)
    {
        int horizontalMove = Random.Range(-1, 2); // Random integer value from -1 to 1 (including).
        int verticalMove = Random.Range(-1, 2);   // Random integer value from -1 to 1 (including).
        movementDirection = new Vector2Int(horizontalMove, verticalMove);
    }


    public void DoAgressiveMovements(ref Vector2Int movementDirection)
    {
        Node start = Node.GetNode(transform.position);
        Node end = Node.GetNode(PlayerLogicBehaviour.Instance.transform.position);

        if (start == end)
        {
            movementDirection = Vector2Int.zero;
            return;
        }

        Node nextNodeToMove = FindPath
        (
            start,
            end,
            NodeMapManager.Instance.NodeMap,
            MapManager.Instance.width,
            MapManager.Instance.height
        );

        NodeMapManager.Instance.NodeMapStatesSetup();

        if (nextNodeToMove)
        {
            int horizontalMove = (int)(nextNodeToMove.transform.position.x - transform.position.x);
            int verticalMove = (int)(nextNodeToMove.transform.position.y - transform.position.y);
            movementDirection = new Vector2Int(horizontalMove, verticalMove);
        }
        else
        {
            DoRandomMovements(ref movementDirection);
        }
    }


    public bool PlayerInFieldOfView()
    {
        float offset = 0.5f;
        float fieldOfViewWidth  = 2f * (enemyLogic.FieldOfViewRadiusX + offset);
        float fieldOfViewHeight = 2f * (enemyLogic.FieldOfViewRadiusY + offset);

        ContactFilter2D contactFilter = new ContactFilter2D() { layerMask = LayerMask.NameToLayer("BlockingLayer") };
        Vector3 overlapBoxSize = new Vector3(fieldOfViewWidth, fieldOfViewHeight);
        Collider2D[] colliders = new Collider2D[32];

        Physics2D.OverlapBox(transform.position, overlapBoxSize, 0f, contactFilter, colliders);

        foreach (Collider2D collider in colliders)
        {
            if (collider)
            {
                if (collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }


    public static Node FindPath(Node start, Node end, Node[,] map, int width, int height)
    {
        int x, y, state = 0, step = 0;
        map[end.X, end.Y].State = 0;

        if (start.X - 1 >= 0)
        {
            if ((int)map[start.X - 1, start.Y].State == -2)
            {
                step++;
            }
        }
        else
        {
            step++;
        }

        if (start.Y - 1 >= 0)
        {
            if ((int)map[start.X, start.Y - 1].State == -2)
            {
                step++;
            }
        }
        else
        {
            step++;
        }

        if (start.X + 1 < width)
        {
            if ((int)map[start.X + 1, start.Y].State == -2)
            {
                step++;
            }
        }
        else
        {
            step++;
        }

        if (start.Y + 1 < height)
        {
            if ((int)map[start.X, start.Y + 1].State == -2)
            {
                step++;
            }
        }
        else
        {
            step++;
        }

        if (step == 4)
        {
            return null;
        }
        else
        {
            step = 0;
        }

        while (true)
        {
            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    if ((int)map[x, y].State == step)
                    {
                        if (x - 1 >= 0)
                        {
                            if ((int)map[x - 1, y].State == -1)
                            {
                                state = step + 1;
                                map[x - 1, y].State = (NodeState)state;
                            }
                        }

                        if (y - 1 >= 0)
                        {
                            if ((int)map[x, y - 1].State == -1)
                            {
                                state = step + 1;
                                map[x, y - 1].State = (NodeState)state;
                            }
                        }

                        if (x + 1 < width)
                        {
                            if ((int)map[x + 1, y].State == -1)
                            {
                                state = step + 1;
                                map[x + 1, y].State = (NodeState)state;
                            }
                        }

                        if (y + 1 < height)
                        {
                            if ((int)map[x, y + 1].State == -1)
                            {
                                state = step + 1;
                                map[x, y + 1].State = (NodeState)state;
                            }
                        }
                    }
                }
            }

            step++;

            if ((int)map[start.X, start.Y].State != -1)
            {
                break;
            }
            if (step != state || step > width * height)
            {
                return null;
            }
        }

        List<Node> result = new List<Node>();
        x = start.X;
        y = start.Y;
        step = (int)map[x, y].State;

        while (x != end.X || y != end.Y)
        {
            if (x - 1 >= 0)
            {
                if (map[x - 1, y].State >= 0)
                {
                    if ((int)map[x - 1, y].State < step)
                    {
                        step = (int)map[x - 1, y].State;
                        result.Add(map[x - 1, y]);
                        x--;
                        continue;
                    }
                }
            }

            if (y - 1 >= 0)
            {
                if (map[x, y - 1].State >= 0)
                {
                    if ((int)map[x, y - 1].State < step)
                    {
                        step = (int)map[x, y - 1].State;
                        result.Add(map[x, y - 1]);
                        y--;
                        continue;
                    }
                }
            }

            if (x + 1 < width)
            {
                if (map[x + 1, y].State >= 0)
                {
                    if ((int)map[x + 1, y].State < step)
                    {
                        step = (int)map[x + 1, y].State;
                        result.Add(map[x + 1, y]);
                        x++;
                        continue;
                    }
                }
            }

            if (y + 1 < height)
            {
                if (map[x, y + 1].State >= 0)
                {
                    if ((int)map[x, y + 1].State < step)
                    {
                        step = (int)map[x, y + 1].State;
                        result.Add(map[x, y + 1]);
                        y++;
                        continue;
                    }
                }
            }

            return null;
        }

        return result[0];
    }
}
