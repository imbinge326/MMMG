using UnityEngine;
using DG.Tweening;

public class StartButton : MonoBehaviour
{
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(new Vector2(0,200), 1.5f).SetEase(Ease.OutQuad); // Move to center
    }
}