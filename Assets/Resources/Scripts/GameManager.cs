
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    
    public Transform transTitleUI;
    public Transform transGame;
    public Transform transGameUI;

    public int gameStage;

    private void Start()
    {
        Inst = this;
        
        IntEventSystem.Register(GameEventEnum.GameStart, OnGameStart);
        IntEventSystem.Register(GameEventEnum.GoToTitle, OnGoToTitle);
    }

    private void OnGameStart(object param)
    {
        gameStage = 1;
        
        transTitleUI.gameObject.SetActive(false);
        transGame.gameObject.SetActive(true);
        transGameUI.gameObject.SetActive(true);
        
        IntEventSystem.Send(GameEventEnum.BlockGameStart, null);
    }

    private void OnGoToTitle(object param)
    {
        gameStage = 0;
        
        transTitleUI.gameObject.SetActive(true);
        transGame.gameObject.SetActive(false);
        transGameUI.gameObject.SetActive(false);
    }
}