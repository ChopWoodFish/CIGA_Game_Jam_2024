
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlateformGameUI : MonoBehaviour
{
    public Button btnReturnTitle;

    private void Start()
    {
        btnReturnTitle.onClick.AddListener(OnBtnReturnTitleClick);
    }

    private void OnBtnReturnTitleClick()
    {
        IntEventSystem.Send(GameEventEnum.GoToTitle, null);
    }
}