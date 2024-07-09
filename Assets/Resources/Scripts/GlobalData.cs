using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "ScriptableObject/GlobalData", order = 0)]
public class GlobalData : ScriptableObject
{
    public GameObject playerBlock;
    public GameObject passBlock;
    public GameObject canBlock;
    public GameObject npcBlock;
    public Sprite playerLihui;
    public GameObject deathPrefab;
    public TextAsset deathDialogue1;
    public TextAsset endCutsceneDialogue;
    public TextAsset deathDialogue2;
}