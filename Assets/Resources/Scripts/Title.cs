using UnityEngine;
using UnityEngine.UI;

// 标题界面UI
public class Title : MonoBehaviour
{
    public Button btnStart;         // 开始按钮
    public Button btnCredits;       // 制作成员按钮
    public Transform transCredits;  // 制作成员界面
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
    }

    private void OnBtnStartClick()
    {
        Debug.Log("Game Start");
        // todo send game start event
        
        
    }

    private void OnBtnCreditClick()
    {
        transCredits.gameObject.SetActive(true);
    }

    private void OnBtnReturnClick()
    {
        transCredits.gameObject.SetActive(false);
    }
}
