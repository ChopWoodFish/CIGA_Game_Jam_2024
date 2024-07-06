
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockPicker
{
    private static List<GameObject> listBlockPrefab = new List<GameObject>();

    public static void CollectPrefab()
    {
        if (listBlockPrefab.Count == 0)
        {
            listBlockPrefab = Resources.LoadAll<GameObject>("Prefabs/SpritePrefab/Block").ToList();
            Debug.Log($"block prefab count: {listBlockPrefab.Count}");
        }
    }

    public static GameObject SelectRandomBlock()
    {
        CollectPrefab();
        int index = Random.Range(0, listBlockPrefab.Count);
        return listBlockPrefab[index];
    }
}