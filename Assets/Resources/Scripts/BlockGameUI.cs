
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockGameUI : MonoBehaviour
{
    public Button btnReturn;
    public Button btnSetting;
    public Button btnStartPlatform;

    public List<PreviewSlot> listPreviewSlot = new List<PreviewSlot>();

    private void Awake()
    {
        IntEventSystem.Register(GameEventEnum.RefreshBlockPreview, RefreshBlockPreview);
    }

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

    private void RefreshBlockPreview(object param)
    {
        List<GameObject> listBlockPrefab = param as List<GameObject>;
        for (int i = 0; i < listBlockPrefab.Count; i++)
        {
            listPreviewSlot[i].SetBlockPreview(listBlockPrefab[i]);
        }
    }
}