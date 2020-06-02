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
using UnityEngine.EventSystems;


public class UIButtonStateHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Variables

    [SerializeField] private Vector3 offsetVector = new Vector3(0f, -15f, 0f);

    private RectTransform textRectTransform;
    private Vector3 textDeafultPosition;

    #endregion Variables


    private void Awake()
    {
        textRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        textRectTransform.localPosition -= offsetVector;
        textDeafultPosition = textRectTransform.localPosition;
    }


    public void OnPointerDown(PointerEventData eventData) => textRectTransform.localPosition = textDeafultPosition + offsetVector;


    public void OnPointerUp(PointerEventData eventData) => textRectTransform.localPosition = textDeafultPosition;
}
