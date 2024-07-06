
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObject/MapData", order = 0)]
public class MapInitBlockSO : ScriptableObject
{
    public int mapIndex;
    public Vector2Int playerBlockPos;
    public Vector2Int passBlockPos;
}