using System;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Inst;

    public TextBubble TextBubble;
    private TalkingCharacter talkingCharacter;
    private InkDialogue dialogue;

    public bool isTalking { get; private set; }
    private Action OnReturnControl;

    // private PlayerInputControl input => GameManager.InputControl.input;

    private void Start()
    {
        Inst = this;
    }

    public void OnTalk(TalkingCharacter chara, Action OnRetControl)
    {
        if (!isTalking)
        {
            if (chara == null)
            {
                Debug.LogError("null TalkingCharacter");
                return;
            }

            Debug.Log(chara.enabled);
            if (!chara.enabled)
            {
                Debug.Log("TalkingCharacter is not enable");
                return;
            }

            OnReturnControl = OnRetControl;
            isTalking = true;
            // GameManager.InputControl.SetAllInputActionDisableExcept(input.Gameplay.Talk, true);
            talkingCharacter = chara;

            dialogue = new InkDialogue(chara.inkJSONAsset);

            // Inst.textBubble.SetBubblePos(Inst.talkingCharacter.bubblePoint); // 对话框位置暂时固定
            // textBubble.gameObject.SetActive(true);
            TextBubble.Show();

            // var playerX = GameManager.GetCharacter("Shangui").transform.position.x;
            // var npcX = talkingCharacter.transform.position.x;
            // var npcScale = talkingCharacter.transform.localScale;
            // npcScale.x = Mathf.Abs(npcScale.x) * (playerX < npcX ? -1 : 1);
            // talkingCharacter.transform.localScale = npcScale;

            TalkOneSentence();
        }
        else
        {
            TalkOneSentence();
        }
    }

    public void ContinueTalk()
    {
        TalkOneSentence();
    }

    private string crtCharaName;
    private void TalkOneSentence()
    {
        string content = dialogue.GetNextSentence();
        if (string.IsNullOrEmpty(content))
        {
            if (dialogue.listCrtChoice.Count == 0)
            {
                FinishTalk();
            }
            else
            {
                Debug.LogWarning("[TalkManager] waiting choice");
                // foreach (var choice in Inst.dialogue.listCrtChoice)
                // {
                //     Debug.Log($"choice {choice.index}: {choice.text}, tag: {choice.tags}");
                // }
            }
        }
        else
        {
            TextBubble.SetBubbleContent(content, dialogue.listCrtChoice);
            if (dialogue.crtTag.ContainsKey("chara"))
            {
                crtCharaName = dialogue.crtTag["chara"];
                TextBubble.SetName(crtCharaName);
            }

            if (crtCharaName == "旁白")
            {
                TextBubble.SetImgChara(null);
            }
            else if (crtCharaName == "小女孩")
            {
                TextBubble.SetImgChara(DataManager.GetGlobalDataSO().playerLihui);
            }
            else
            {
                TextBubble.SetImgChara(talkingCharacter.listLihui[0]);
                // if (dialogue.crtTag.ContainsKey("image"))
                // {
                    // var sprName = crtCharaName;
                    // int index = talkingCharacter.listLihuiName.IndexOf(sprName);
                    // if (index >= 0)
                    // {
                    //     var spr = talkingCharacter.listLihui[index];
                    //     TextBubble.SetImgChara(spr);
                    // }
                // }
                // else
                // {
                //     TextBubble.SetImgChara(null);
                // }
            }
            talkingCharacter.ProcessTag(dialogue.crtTag);
        }
    }

    private void FinishTalk()
    {
        isTalking = false;
        // GameManager.InputControl.RestoreInputActionEnableState();
        TextBubble.Hide();
        
        Debug.Log("[TalkManager] FinishTalk");
        // IntEventSystem.Send(GameEventEnum.FinishTalk, talkingCharacter);

        OnReturnControl?.Invoke();
    }

    public void ChooseChoice(int index)
    {
        dialogue.ChooseChoice(index);
        TalkOneSentence();
    }

    public void Discard()
    {
        Inst = null;
    }
}