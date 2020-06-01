using UnityEngine;
using UnityEngine.EventSystems;


public class UIBombButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    #region Variables

    [SerializeField] private float maxTimeBetweenClicks = 0.5f;

    private float lastTimeClick = 0f;

    #endregion Variables


    public void OnPointerClick(PointerEventData eventData)
    {
        float currentTimeClick = eventData.clickTime;

        if (currentTimeClick - lastTimeClick < maxTimeBetweenClicks)
        {
            PlayerBombDroppingBehaviour.Instance.DropBomb();
        }

        lastTimeClick = currentTimeClick;
    }
}
