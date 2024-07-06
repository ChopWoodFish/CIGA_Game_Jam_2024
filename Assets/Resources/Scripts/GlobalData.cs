using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "ScriptableObject/GlobalData", order = 0)]
public class GlobalData : ScriptableObject
{
    public GameObject playerBlock;
    public GameObject passBlock;
}