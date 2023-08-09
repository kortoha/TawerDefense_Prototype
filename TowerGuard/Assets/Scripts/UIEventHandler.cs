using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerDownHandler
{
    public static bool IsPointerOverUI { get; private set; } = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPointerOverUI = true;
    }
}