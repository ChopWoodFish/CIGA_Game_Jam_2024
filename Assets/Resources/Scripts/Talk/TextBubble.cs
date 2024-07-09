using System;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpName;
    [SerializeField] private TextMeshProUGUI tmpContent;
    [SerializeField] private Image imgChara;
    [SerializeField] private List<Button> listBtnChoice = new List<Button>();
    [SerializeField] private Button btnSkip;
    private Transform point;

    private RectTransform UIRT;
    private Camera UICamera;

    private void Awake()
    {
        UIRT = transform.parent as RectTransform;
        UICamera = Camera.main;

        foreach (var btnChoice in listBtnChoice)
        {
            btnChoice.gameObject.SetActive(false);
        }
        
        btnSkip.onClick.AddListener(() =>
        {
            IntEventSystem.Send(GameEventEnum.SkipTalk, null);
        });
    }

    public void SetBubblePos(Transform point)
    {
        this.point = point;
    }

    public void SetBubbleContent(string content, List<Choice> listChoice)
    {
        content = content.Replace("\\n", "\n");
        tmpContent.text = content;

        foreach (var btnChoice in listBtnChoice)
        {
            btnChoice.gameObject.SetActive(false);
        }

        if (listChoice == null)
        {
            return;
        }
        
        for (int i = 0; i < listChoice.Count; i++)
        {
            int index = i;
            var choice = listChoice[i];
            var btnChoice = listBtnChoice[i];
            btnChoice.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            btnChoice.onClick.RemoveAllListeners();
            btnChoice.onClick.AddListener(() => { TalkManager.Inst.ChooseChoice(index); });
            btnChoice.gameObject.SetActive(true);
        }
    }

    public void SetName(string name)
    {
        tmpName.text = name;
    }

    public void SetImgChara(Sprite spr)
    {
        if (spr == null)
        {
            imgChara.gameObject.SetActive(false);
        }
        else
        {
            imgChara.gameObject.SetActive(true);
            imgChara.sprite = spr;
        }
    }

    private void Update()
    {
        // 将定位点从世界坐标转换为ui坐标，赋给气泡
        // var bubbleScreenPos =
            // RectTransformUtility.WorldToScreenPoint(UICamera, point.position);
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(UIRT, bubbleScreenPos, null,
            // out var bubbleUIPos);
        // Debug.Log($"bubble wp: {textBubble.transform.position}, bubble lp: {textBubble.transform.localPosition}, point wp: {talkingCharacter.bubblePoint.position}");
        // Debug.Log($"point sp: {bubbleScreenPos}");
        // Debug.Log($"point uip: {bubbleUIPos}");
        // transform.localPosition = bubbleUIPos;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        tmpContent.text = "";
        gameObject.SetActive(false);
    }
}