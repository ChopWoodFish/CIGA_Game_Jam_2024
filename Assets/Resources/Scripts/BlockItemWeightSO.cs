using UnityEngine;

[CreateAssetMenu(fileName = "BlockItemWeight", menuName = "ScriptableObject/BlockItemWeight", order = 0)]
public class BlockItemWeightSO : ScriptableObject
{
    public float emptyBlockWeight;
    public float baseBlockWeight;
    public float trapBlockWeight;
    public float fragileBlockWeight;

    public GameObject emptyBlockPrefab;
    public GameObject baseBlockPrefab;
    public GameObject trapBlockPrefab;
    public GameObject fragileBlockPrefab;

    public GameObject GetRandomBlockPrefab()
    {
        float totalWeight = emptyBlockWeight + baseBlockWeight + trapBlockWeight + fragileBlockWeight;
        float rand = Random.Range(0, totalWeight);
        float threshold = emptyBlockWeight;
        if (rand < threshold)
        {
            return emptyBlockPrefab;
        }

        threshold += baseBlockWeight;
        if (rand < threshold)
        {
            return baseBlockPrefab;
        }

        threshold += trapBlockWeight;
        if (rand < threshold)
        {
            return trapBlockPrefab;
        }

        threshold += fragileBlockWeight;
        if (rand < threshold)
        {
            return fragileBlockPrefab;
        }

        return emptyBlockPrefab;
    }
}