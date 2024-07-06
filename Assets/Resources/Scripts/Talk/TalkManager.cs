using System;
using UnityEngine;

public class TalkManager
{
    private static TalkManager Inst;

    private TextBubble TextBubble => GameManager.UIMan.TextBox;
    private TalkingCharacter talkingCharacter;
    private InkDialogue dialogue;

    public bool isTalking { get; private set; }
    private Action OnReturnControl;

    private PlayerInputControl input => GameManager.InputControl.input;

    private TalkManager(GameManager gm)
    {
        try
        {
            TextBubble.Hide();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public static TalkManager CreateInst(GameManager gm)
    {
        if (Inst != null)
        {
            Inst.Discard();
        }

        Inst = new TalkManager(gm);
        return Inst;
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
            GameManager.InputControl.SetAllInputActionDisableExcept(input.Gameplay.Talk, true);
            talkingCharacter = chara;

            dialogue = new InkDialogue(chara.inkJSONAsset);

            // Inst.textBubble.SetBubblePos(Inst.talkingCharacter.bubblePoint); // 对话框位置暂时固定
            // textBubble.gameObject.SetActive(true);
            TextBubble.Show();

            var playerX = GameManager.GetCharacter("Shangui").transform.position.x;
            var npcX = talkingCharacter.transform.position.x;
            var npcScale = talkingCharacter.transform.localScale;
            npcScale.x = Mathf.Abs(npcScale.x) * (playerX < npcX ? -1 : 1);
            talkingCharacter.transform.localScale = npcScale;

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
            talkingCharacter.ProcessTag(dialogue.crtTag);
        }
    }

    private void FinishTalk()
    {
        isTalking = false;
        GameManager.InputControl.RestoreInputActionEnableState();
        TextBubble.Hide();
        
        Debug.Log("[TalkManager] FinishTalk");
        IntEventSystem.Send(GameEventEnum.FinishTalk, talkingCharacter);

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