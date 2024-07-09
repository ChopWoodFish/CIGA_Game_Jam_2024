using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 标题界面UI
public class Title : MonoBehaviour
{
    public Image imgBg;
    public Image imgBgEnd;
    public VideoUI videoUI;
    public Button btnStart;         // 开始按钮
    public Button btnTuto;
    public Transform transTuto;
    public Image imgTuto;
    public List<Sprite> listSprTuto = new List<Sprite>();
    private int tutoIndex;
    public Button btnPrevTuto;
    public Button btnNextTuto;
    public Button btnReturn2;
    public Button btnCredits;       // 制作成员按钮
    public Transform transCredits;  // 制作成员界面
    public Button btnEixt;          // 退出游戏按钮
    public Button btnReturn;        // 返回标题界面按钮

    private void Awake()
    {
        IntEventSystem.Register(GameEventEnum.ChangeCG, OnChangeCG);
        transCredits.gameObject.SetActive(false);
    }

    private void OnChangeCG(object param)
    {
        if ((string) param == "3")
        {
            imgBg.gameObject.SetActive(false);
            imgBgEnd.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        btnStart.onClick.AddListener(OnBtnStartClick);
        btnTuto.onClick.AddListener(OnBtnTutoClick);
        btnCredits.onClick.AddListener(OnBtnCreditClick);
        btnReturn.onClick.AddListener(OnBtnReturnClick);
        btnEixt.onClick.AddListener(OnBtnExitClick);
        btnPrevTuto.onClick.AddListener(() =>
        {
            OnTutoChange(false);
        });
        btnNextTuto.onClick.AddListener(() =>
        {
            OnTutoChange(true);
        });
        btnReturn2.onClick.AddListener(OnBtnReturnClick);
        
        IntEventSystem.Register(GameEventEnum.FinishPlayVideo, OnVideoFinish);
    }
    
    private void OnBtnTutoClick()
    {
        transTuto.gameObject.SetActive(true);
    }

    private void OnTutoChange(bool isNext)
    {
        if (isNext)
        {
            tutoIndex = (tutoIndex + 1) % listSprTuto.Count;
        }
        else
        {
            tutoIndex = (tutoIndex + listSprTuto.Count - 1) % listSprTuto.Count;
        }

        imgTuto.sprite = listSprTuto[tutoIndex];
    }

    private void OnBtnStartClick()
    {
        videoUI.gameObject.SetActive(true);
        videoUI.SetVideo();
        videoUI.PlayVideo();
    }

    private void OnVideoFinish(object param)
    {
        videoUI.gameObject.SetActive(false);
        IntEventSystem.Send(GameEventEnum.GameStart, null);
    }

    private void OnBtnCreditClick()
    {
        transCredits.gameObject.SetActive(true);
    }

    private void OnBtnReturnClick()
    {
        transCredits.gameObject.SetActive(false);
        transTuto.gameObject.SetActive(false);
    }

    private void OnBtnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
