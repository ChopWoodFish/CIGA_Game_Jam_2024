using System;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    private Trigger2DCheck npcChecker;

    private void Awake()
    {
        npcChecker = GetComponent<Trigger2DCheck>();
    }

    private void Update()
    {
        if (GameManager.Inst.gameStage != 2)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Check Npc");
            npcChecker.Check();
            if (npcChecker.HasHit)
            {
                var listHits = npcChecker.GetAllHitThings();
                foreach (var hitGO in listHits)
                {
                    TalkingCharacter talkChara = hitGO.GetComponent<TalkingCharacter>();
                    if (talkChara != null)
                    {
                        TalkManager.Inst.OnTalk(talkChara, () => {Debug.Log("Talk Finish");});
                        return;
                    }
                }
            }
        }
            
    }
}