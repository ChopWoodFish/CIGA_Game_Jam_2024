using System;
using Unity.VisualScripting;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Inst;

    public TextBubble textBubble;
    public TextBubble cutsceneTextBubble;
    private TalkingCharacter talkingCharacter;
    private InkDialogue dialogue;

    public bool isTalking { get; private set; }
    private Action OnReturnControl;
    private TextBubble crtTextBubble;

    // private PlayerInputControl input => GameManager.InputControl.input;

    private void Start()
    {
        Inst = this;
        IntEventSystem.Register(GameEventEnum.SkipTalk, (param) =>
        {
            FinishTalk();
        });
    }

    public void OnTalkCutscene(TalkingCharacter chara, Action OnRetControl)
    {
        isTalking = true;
        OnReturnControl = OnRetControl;
        int mapIndex = GameManager.Inst.mapIndex;
        if (chara == null)
        {
            crtTextBubble = cutsceneTextBubble;
            talkingCharacter = null;
            if (mapIndex != 4)
            {
                var textAsset = DataManager.GetMapInitBlockSO(mapIndex).cutsceneText;
                dialogue = new InkDialogue(textAsset);
            }
            else
            {
                var textAsset = DataManager.GetGlobalDataSO().endCutsceneDialogue;
                dialogue = new InkDialogue(textAsset);
            }
        }
        else
        {
            crtTextBubble = textBubble;
            talkingCharacter = chara;
            var textAsset = mapIndex == 1
                ? DataManager.GetGlobalDataSO().deathDialogue1
                : DataManager.GetGlobalDataSO().deathDialogue2;
            dialogue = new InkDialogue(textAsset);
        }

        crtTextBubble.Show();
        TalkOneSentence();
    }

    private void Update()
    {
        if (!GameManager.Inst.isGameEnd && GameManager.Inst.gameStage != 1)
        {
            return;
        }

        if (!isTalking)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TalkOneSentence();   
        }
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
            crtTextBubble = textBubble;
            // GameManager.InputControl.SetAllInputActionDisableExcept(input.Gameplay.Talk, true);
            talkingCharacter = chara;

            dialogue = new InkDialogue(chara.GetDialogueAsset());

            // Inst.textBubble.SetBubblePos(Inst.talkingCharacter.bubblePoint); // 对话框位置暂时固定
            // textBubble.gameObject.SetActive(true);
            crtTextBubble.Show();

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
            crtTextBubble.SetBubbleContent(content, dialogue.listCrtChoice);

            if (dialogue.crtTag.ContainsKey("cg"))
            {
                IntEventSystem.Send(GameEventEnum.ChangeCG, dialogue.crtTag["cg"]);
            }
            
            if (dialogue.crtTag.ContainsKey("chara"))
            {
                crtCharaName = dialogue.crtTag["chara"];
                crtTextBubble.SetName(crtCharaName);
            }

            if (crtCharaName == "旁白")
            {
                crtTextBubble.SetImgChara(null);
            }
            else if (crtCharaName == "小女孩")
            {
                crtTextBubble.SetImgChara(DataManager.GetGlobalDataSO().playerLihui);
            }
            else
            {
                if (talkingCharacter != null)
                {
                    crtTextBubble.SetImgChara(talkingCharacter.listLihui[0]);
                }
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
            // talkingCharacter.ProcessTag(dialogue.crtTag);
        }
    }

    private void FinishTalk()
    {
        isTalking = false;
        // GameManager.InputControl.RestoreInputActionEnableState();
        crtTextBubble.Hide();
        
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