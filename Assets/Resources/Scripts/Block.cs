using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform transBg;
    public float blockItemSize = 1f;

    public int ID;
    public int width;
    public int height;

    public List<Vector2Int> localPosOffset = new List<Vector2Int>();

    private void Awake()
    {
        // localPosOffset.Add(new Vector2Int(0, 0));
        for (int i = 0; i < transBg.childCount; i++)
        {
            var blockItem = transBg.GetChild(i);
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
        }
        width += 1;
        height += 1;
        Debug.Log($"Block {ID} width: {width}, height: {height}");
    }
}