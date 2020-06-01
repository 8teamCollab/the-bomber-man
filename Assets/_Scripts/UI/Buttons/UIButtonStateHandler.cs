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
