using UnityEngine;
using UnityEngine.UI;

// 标题界面UI
public class Title : MonoBehaviour
{
    public VideoUI videoUI;
    public Button btnStart;         // 开始按钮
    public Button btnCredits;       // 制作成员按钮
    public Transform transCredits;  // 制作成员界面
    public Button btnEixt;          // 退出游戏按钮
    public Button btnReturn;        // 返回标题界面按钮

    private void Awake()
    {
        transCredits.gameObject.SetActive(false);
    }

    private void Start()
    {
        btnStart.onClick.AddListener(OnBtnStartClick);
        btnCredits.onClick.AddListener(OnBtnCreditClick);
        btnReturn.onClick.AddListener(OnBtnReturnClick);
        btnEixt.onClick.AddListener(OnBtnExitClick);
        
        IntEventSystem.Register(GameEventEnum.FinishPlayVideo, OnVideoFinish);
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
