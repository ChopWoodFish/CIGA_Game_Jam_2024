using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneUI : MonoBehaviour
{
    public List<Image> listImgCG = new List<Image>();

    private void Start()
    {
        IntEventSystem.Register(GameEventEnum.ChangeCG, OnChangeCG);
        IntEventSystem.Register(GameEventEnum.GoToTitle, OnGoToTitle);
    }

    private void OnChangeCG(object param)
    {
        string s = (string) param;
        int index = int.Parse(s);
        for (int i = 0; i < listImgCG.Count; i++)
        {
            if (i == index - 1)
            {
                listImgCG[i].gameObject.SetActive(true);
            }
            else
            {
                listImgCG[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnGoToTitle(object param)
    {
        foreach (var imgCG in listImgCG)
        {
            imgCG.gameObject.SetActive(false);
        }
    }
}