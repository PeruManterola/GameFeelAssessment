using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsPanel : MonoBehaviour
{
    public Sprite closeSprite;
    public Sprite openSprite;
    public Image buttonImage;
    private bool isOpen = true;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ManagePanel()
    {
        if (isOpen)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

    private void ClosePanel()
    {
        rectTransform.DOAnchorPosX(-176, 2f);
        isOpen = false;
        buttonImage.sprite = openSprite;
    }

    private void OpenPanel()
    {
        rectTransform.DOAnchorPosX(180, 2f);
        isOpen = true;
        buttonImage.sprite = closeSprite;
    }
}
