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
using UnityEngine.UI;


public class EnemyFieldOfViewRenderer : MonoBehaviour
{
    #region Variables

    [SerializeField] private float alpha = 0.15f;

    [HideInInspector] public bool colorChanging;

    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;
    private EnemyLogicBehaviour enemyLogic;

    private float offset = 0.5f;

    #endregion Variables


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer   = GetComponent<LineRenderer>();
        enemyLogic          = GetComponent<EnemyLogicBehaviour>();

        SetupColors();
        SetupFieldOfViewImage();
    }


    private void Update() => RenderFieldOfViewBorder();


    private void RenderFieldOfViewBorder()
    {
        float x = enemyLogic.FieldOfViewRadiusX + offset;
        float y = enemyLogic.FieldOfViewRadiusY + offset;
        float z = -0.1f;

        lineRenderer.SetPositions
        (new Vector3[]
            {
                new Vector3(transform.position.x - x, transform.position.y - y, z),
                new Vector3(transform.position.x - x, transform.position.y + y, z),
                new Vector3(transform.position.x + x, transform.position.y + y, z),
                new Vector3(transform.position.x + x, transform.position.y - y, z)
            }
        );
    }


    private void SetupColors()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        if (colorChanging)
        {
            spriteRenderer.color = randomColor;
            lineRenderer.material.color = spriteRenderer.color;
        }
        else
        {
            lineRenderer.material.color = randomColor;
        }
    }


    private void SetupFieldOfViewImage()
    {
        RectTransform enemyCanvasRectTransform = GetComponentInChildren<RectTransform>();
        Image fieldOfViewImage = enemyCanvasRectTransform.GetComponentInChildren<Image>();

        float width = 2f * (enemyLogic.FieldOfViewRadiusX + offset);
        float height = 2f * (enemyLogic.FieldOfViewRadiusY + offset);
        enemyCanvasRectTransform.sizeDelta = new Vector2(width, height);

        Color alphaColor = new Color(0f, 0f, 0f, 1f - alpha);

        if (colorChanging)
        {
            fieldOfViewImage.color = spriteRenderer.color - alphaColor;
        }
        else
        {
            fieldOfViewImage.color = lineRenderer.material.color - alphaColor;
        }
    }
}
