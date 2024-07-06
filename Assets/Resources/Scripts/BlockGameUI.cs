
using System;
using UnityEngine;
using UnityEngine.UI;

public class BlockGameUI : MonoBehaviour
{
    public Button btnReturn;
    public Button btnSetting;

    private void Start()
    {
        btnReturn.onClick.AddListener(OnBtnReturnClick);
        btnSetting.onClick.AddListener(OnBtnSettingClick);
    }

    private void OnBtnReturnClick()
    {
        IntEventSystem.Send(GameEventEnum.GoToTitle, null);
    }

    private void OnBtnSettingClick()
    {
        
    }
}