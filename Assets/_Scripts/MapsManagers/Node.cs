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
