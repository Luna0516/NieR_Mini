using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    TextMeshProUGUI clearText;
    TextMeshProUGUI playTimeText;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        clearText = child.GetChild(0).GetComponent<TextMeshProUGUI>();
        playTimeText = child.GetChild(2).GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();

        SetActive(false);
    }

    private void Start()
    {
        GameManager.Inst.onGameEnd += ChangeText;
    }

    private void ChangeText(bool isClear, float playTime)
    {
        SetActive(true);
        clearText.text = isClear ? $"클리어!!!" : $"실패!!!";
        playTimeText.text = $"{playTime:F2}초";
    }

    private void SetActive(bool active)
    {
        if (active)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
