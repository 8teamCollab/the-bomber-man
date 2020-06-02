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


public class CameraController : MonoBehaviour
{
    #region Variables

    [Header("Margins")]
    [SerializeField] [Range(0f, 10f)] private float xMargin = 0f;
    [SerializeField] [Range(0f, 10f)] private float yMargin = 0f;

    [Header("Smoothness")]
    [SerializeField] [Range(0f, 10f)] private float xSmooth = 4.5f;
    [SerializeField] [Range(0f, 10f)] private float ySmooth = 4.5f;

    private float boundsMinX;
    private float boundsMinY;
    private float boundsMaxX;
    private float boundsMaxY;

    private Transform player;
    new private Camera camera;

    #endregion Variables


    private void Start()
    {
        camera = GetComponent<Camera>();

        boundsMinX = -1f;
        boundsMinY = -1f;
        boundsMaxX = MapManager.Instance.width + 1f;
        boundsMaxY = MapManager.Instance.height + 1f;
    }


    private void LateUpdate()
    {
        player = PlayerLogicBehaviour.Instance.transform;

        if (player)
        {
            TrackPlayer();
        }
    }


    private bool CheckXMargin() => Mathf.Abs(transform.position.x - player.position.x) > xMargin;


    private bool CheckYMargin() => Mathf.Abs(transform.position.y - player.position.y) > yMargin;


    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        float camVertExtent = camera.orthographicSize;
        float camHorzExtent = camera.aspect * camVertExtent;

        float leftBound = boundsMinX + camHorzExtent;
        float rightBound = boundsMaxX - camHorzExtent;
        float bottomBound = boundsMinY + camVertExtent;
        float topBound = boundsMaxY - camVertExtent;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, leftBound, rightBound);
        targetY = Mathf.Clamp(targetY, bottomBound, topBound);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
