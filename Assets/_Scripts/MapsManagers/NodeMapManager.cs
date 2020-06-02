/*
 * Copyright(C) 2020 Artyom Bezmenov (8nhuman8)

 * This file is part of The Bomber Man.

 * The Bomber Man is free software: you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this The Bomber Man. If not,
 * see <https://www.gnu.org/licenses/>.
 */


using UnityEngine;
using UnityEngine.Tilemaps;


public class NodeMapManager : SingletonBehaviour<NodeMapManager>
{
    #region Variables

    public Node[,] NodeMap;


    [SerializeField] private GameObject nodeGameObject;

    [Header("Tile references")]
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase boxTile;

    private Tilemap tilemapGameplay;

    #endregion Variables


    protected override void Awake()
    {
        base.Awake();

        tilemapGameplay = GameObject.Find("/Grid/TilemapGameplay").GetComponent<Tilemap>();
    }


    public void NodeMapSetup()
    {
        int width = MapManager.Instance.width;
        int height = MapManager.Instance.height;
        NodeMap = new Node[width, height];

        Transform nodeMapHolder = new GameObject("NodeMap").transform;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float offset = 0.5f;
                Vector3 spawnPosition = new Vector3(x + offset, y + offset);

                GameObject nodeInstance = Instantiate(nodeGameObject, spawnPosition, Quaternion.identity, nodeMapHolder) as GameObject;
                nodeInstance.name = $"Node ({x}, {y})";

                Node node = nodeInstance.GetComponent<Node>();
                node.X = x;
                node.Y = y;
                NodeMap[x, y] = node;
            }
        }
    }


    public void NodeMapStatesSetup()
    {
        foreach (Node node in NodeMap)
        {
            Vector3 nodePosition = node.transform.position;
            Vector3Int nodeCellPosition = tilemapGameplay.WorldToCell(nodePosition);
            TileBase tile = tilemapGameplay.GetTile(nodeCellPosition);

            GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
            bool isPositionOfBomb = false;

            foreach (GameObject bomb in bombs)
            {
                isPositionOfBomb = nodePosition == bomb.transform.position;
                break;
            }

            if (tile == wallTile || tile == boxTile || isPositionOfBomb)
            {
                node.State = NodeState.NotWalkable;
            }
            else
            {
                node.State = NodeState.Walkable;
            }
        }
    }
}
