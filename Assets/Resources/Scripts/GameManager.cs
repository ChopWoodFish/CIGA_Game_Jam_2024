using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    
    public Transform transTitleUI;
    public Transform transCutsceneUI;
    public Transform transGame;
    public Transform transGameUI;
    public Transform transGameUI2;

    public int mapIndex = 1;
    public int gameStage;   // 0-标题页 1-过场对话 2-方块游戏 3-平台游戏
    public CinemachineVirtualCamera vcStage1;
    public CinemachineVirtualCamera vcStage2;
    public bool isGameEnd;

    private List<Transform> listTransUI = new List<Transform>();
    
    private void Start()
    {
        Inst = this;
        
        listTransUI.Clear();
        listTransUI.Add(transTitleUI);
        listTransUI.Add(transCutsceneUI);
        listTransUI.Add(transGameUI);
        listTransUI.Add(transGameUI2);
        
        IntEventSystem.Register(GameEventEnum.GameStart, OnGameStart);
        IntEventSystem.Register(GameEventEnum.GoToTitle, OnGoToTitle);
        IntEventSystem.Register(GameEventEnum.PlatformGameStart, OnPlatformGameStart);
        IntEventSystem.Register(GameEventEnum.GoToNextMap, GoToNextMap);
        IntEventSystem.Register(GameEventEnum.CutsceneStart, OnCutsceneStart);
    }

    private void CloseAllUI()
    {
        foreach (var transUI in listTransUI)
        {
            transUI.gameObject.SetActive(false);
        }
    }

    private void OnGameStart(object param)
    {
        gameStage = 1;
        IntEventSystem.Send(GameEventEnum.CutsceneStart, null);
    }

    private void OnCutsceneStart(object param)
    {
        if (mapIndex == 4 && !isGameEnd)
        {
            StartBlockGame();
            return;
        }
        
        CloseAllUI();
        transCutsceneUI.gameObject.SetActive(true);

        if (mapIndex != 4)
        {
            TalkManager.Inst.OnTalkCutscene(null, () =>
            {
                Debug.Log("Cutscene Talk Finish");
                if (mapIndex == 1 || mapIndex == 4)
                {
                    TalkManager.Inst.OnTalkCutscene(
                        DataManager.GetGlobalDataSO().deathPrefab.GetComponent<TalkingCharacter>(), StartBlockGame);
                }
                else
                {
                    StartBlockGame();
                }
            });
        }
        else
        {
            TalkManager.Inst.OnTalkCutscene(null, () =>
            {
                TalkManager.Inst.OnTalkCutscene(
                    DataManager.GetGlobalDataSO().deathPrefab.GetComponent<TalkingCharacter>(), () =>
                    {
                        IntEventSystem.Send(GameEventEnum.GoToTitle, null);
                    });
            });   
        }
    }

    private void StartBlockGame()
    {
        gameStage = 2;
        
        CloseAllUI();
        transGameUI.gameObject.SetActive(true);

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
        
        CloseAllUI();
        transTitleUI.gameObject.SetActive(true);

        vcStage1.gameObject.SetActive(true);
        vcStage2.gameObject.SetActive(false);
        
        bgmManager.instance.PlaystartBgm();
    }

    private void OnPlatformGameStart(object param)
    {
        gameStage = 3;
        
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
        else
        {
            OnGameEnd();
        }
    }

    private void OnGameEnd()
    {
        isGameEnd = true;
        IntEventSystem.Send(GameEventEnum.CutsceneStart, null);
    }
}