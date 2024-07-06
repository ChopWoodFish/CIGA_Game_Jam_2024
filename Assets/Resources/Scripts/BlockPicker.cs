
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockPicker
{
    private List<GameObject> listBlockPrefab = new List<GameObject>();

    public void CollectPrefab()
    {
        listBlockPrefab = Resources.LoadAll<GameObject>("Prefabs/SpritePrefab/Block").ToList();
        Debug.Log($"block prefab count: {listBlockPrefab.Count}");
    }

    public GameObject SelectRandomBlock()
    {
        int index = Random.Range(0, listBlockPrefab.Count);
        return listBlockPrefab[index];
    }
}