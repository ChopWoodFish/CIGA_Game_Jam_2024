
using System;
using UnityEngine;
using UnityEngine.UI;

public class BlockGameUI : MonoBehaviour
{
    public Button btnReturn;
    public Button btnSetting;
    public Button btnStartPlatform;

    private void Start()
    {
        IntEventSystem.Register(GameEventEnum.BlockGameStart, OnBlockGameStart);
        IntEventSystem.Register(GameEventEnum.BlockGameFinish, OnBlockGameFinish);
        
        btnReturn.onClick.AddListener(OnBtnReturnClick);
        btnSetting.onClick.AddListener(OnBtnSettingClick);
        btnStartPlatform.onClick.AddListener(OnBtnStartPlatform);
    }

    private void OnBtnReturnClick()
    {
        IntEventSystem.Send(GameEventEnum.GoToTitle, null);
    }

    private void OnBtnSettingClick()
    {
        
    }
    
    private void OnBtnStartPlatform()
    {
        IntEventSystem.Send(GameEventEnum.PlatformGameStart, null);
    }
    
    private void OnBlockGameStart(object param)
    {
        btnStartPlatform.interactable = false;
    }

    private void OnBlockGameFinish(object param)
    {
        btnStartPlatform.interactable = true;
    }
}