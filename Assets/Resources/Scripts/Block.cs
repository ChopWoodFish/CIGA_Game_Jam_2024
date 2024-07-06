using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform transBg;
    public float blockItemSize = 1f;

    public int ID;
    public int width;
    public int height;

    public List<BlockItem> listBlockItem = new List<BlockItem>();
    public List<Vector2Int> localPosOffset = new List<Vector2Int>();

    private void Awake()
    {
        // localPosOffset.Add(new Vector2Int(0, 0));
        for (int i = 0; i < transBg.childCount; i++)
        {
            var blockItem = transBg.GetChild(i).GetComponent<BlockItem>();
            int tmpWidth = (int) (blockItem.transform.localPosition.x / blockItemSize);
            if (tmpWidth >= width * blockItemSize)
            {
                width = tmpWidth;
            }
            int tmpHeight = (int) (blockItem.transform.localPosition.y / blockItemSize);
            if (tmpHeight >= height * blockItemSize)
            {
                height = tmpHeight;
            }
            localPosOffset.Add(new Vector2Int(tmpHeight, tmpWidth));
            listBlockItem.Add(blockItem);
        }
        width += 1;
        height += 1;
        Debug.Log($"Block {ID} width: {width}, height: {height}");
        
        // GenBlockItem();
    }

    public void GenBlockItem()
    {
        foreach (var itemBg in listBlockItem)
        {
            var itemPrefab = BlockPicker.SelectRandomBlockItem();
            var genItem = Instantiate(itemPrefab, transform);
            // genItem.transform.SetParent(transform);
            genItem.transform.position = itemBg.transform.position;
        }
    }

    public void HideOutline()
    {
        foreach (var blockItem in listBlockItem)
        {
            blockItem.SetOutlineEnable(false);
        }
    }
}