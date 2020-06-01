using UnityEngine;
using UnityEngine.EventSystems;


public class UIButtonPlaySound : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData) => AudioManager.Instance.PlaySound(Sound.ButtonPressed);
}
