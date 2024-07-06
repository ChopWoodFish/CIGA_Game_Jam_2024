
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform transTitleUI;
    public Transform transGame;
    public Transform transGameUI;

    private void Start()
    {
        IntEventSystem.Register(GameEventEnum.GameStart, OnGameStart);
        IntEventSystem.Register(GameEventEnum.GoToTitle, OnGoToTitle);
    }

    private void OnGameStart(object param)
    {
        transTitleUI.gameObject.SetActive(false);
        transGame.gameObject.SetActive(true);
        transGameUI.gameObject.SetActive(true);
        
        IntEventSystem.Send(GameEventEnum.BlockGameStart, null);
    }

    private void OnGoToTitle(object param)
    {
        transTitleUI.gameObject.SetActive(true);
        transGame.gameObject.SetActive(false);
        transGameUI.gameObject.SetActive(false);
    }
}