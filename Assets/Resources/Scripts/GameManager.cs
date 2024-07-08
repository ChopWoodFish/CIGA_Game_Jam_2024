using System.Collections;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    
    public Transform transTitleUI;
    public Transform transGame;
    public Transform transGameUI;
    public Transform transGameUI2;

    public int mapIndex = 1;
    public int gameStage;
    public CinemachineVirtualCamera vcStage1;
    public CinemachineVirtualCamera vcStage2;

    private void Start()
    {
        Inst = this;
        
        IntEventSystem.Register(GameEventEnum.GameStart, OnGameStart);
        IntEventSystem.Register(GameEventEnum.GoToTitle, OnGoToTitle);
        IntEventSystem.Register(GameEventEnum.PlatformGameStart, OnPlatformGameStart);
        IntEventSystem.Register(GameEventEnum.GoToNextMap, GoToNextMap);
    }

    private void OnGameStart(object param)
    {
        gameStage = 1;
        
        transTitleUI.gameObject.SetActive(false);
        transGame.gameObject.SetActive(true);
        transGameUI.gameObject.SetActive(true);
        transGameUI2.gameObject.SetActive(false);
        
        vcStage1.gameObject.SetActive(true);
        vcStage2.gameObject.SetActive(false);
        
        bgmManager.instance.PlayinBgm();
        
        IntEventSystem.Send(GameEventEnum.BlockGameStart, null);
    }

    private void OnGoToTitle(object param)
    {
        StopAllCoroutines();
        mapIndex = 1;
        gameStage = 0;
        
        transTitleUI.gameObject.SetActive(true);
        transGame.gameObject.SetActive(false);
        transGameUI.gameObject.SetActive(false);
        transGameUI2.gameObject.SetActive(false);
        
        vcStage1.gameObject.SetActive(true);
        vcStage2.gameObject.SetActive(false);
        
        bgmManager.instance.PlaystartBgm();
    }

    private void OnPlatformGameStart(object param)
    {
        gameStage = 2;
        
        // transGameUI.gameObject.SetActive(false);
        // transGameUI2.gameObject.SetActive(true);
        StartCoroutine(SwitchBlockGameUI());

        vcStage2.Follow = GameObject.FindWithTag("Player").transform;
        
        vcStage1.gameObject.SetActive(false);
        vcStage2.gameObject.SetActive(true);
    }
    
    IEnumerator SwitchBlockGameUI()
    {
        yield return new WaitForSeconds(0.8f);

        transGameUI.gameObject.SetActive(false);
        transGameUI2.gameObject.SetActive(true);
    }

    private void GoToNextMap(object param)
    {
        int nextMapIndex = mapIndex + 1;
        var mapData = DataManager.GetMapInitBlockSO(nextMapIndex);
        if (mapData != null)
        {
            mapIndex = nextMapIndex;
            Debug.Log($"set map index {mapIndex}");
            OnGameStart(null);
        }
    }
}