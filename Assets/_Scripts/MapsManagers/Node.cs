using UnityEngine;


public class Node : MonoBehaviour
{
    #region Variables

    public int X { get; set; }
    public int Y { get; set; }
    public NodeState State { get; set; }


    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private NodeState state;

    #endregion Variables


    private void Start() { x = X; y = Y; }
    private void Update() => state = State;


    public static Node GetNode(Vector3 position)
    {
        float offset = 0.5f;
        int xPosition = (int)(position.x - offset);
        int yPosition = (int)(position.y - offset);

        return NodeMapManager.Instance.NodeMap[xPosition, yPosition];
    }
}
