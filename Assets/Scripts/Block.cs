using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform transBg;
    public float blockItemSize = 1f;

    public int ID;
    public int width;
    public int height;

    private void Awake()
    {
        for (int i = 0; i < transBg.childCount; i++)
        {
            var blockItem = transBg.GetChild(i);
            int tmpWidth = (int) (blockItem.transform.localPosition.x / blockItemSize);
            if (tmpWidth > width * blockItemSize)
            {
                width = tmpWidth;
            }
            int tmpHeight = (int) (blockItem.transform.localPosition.y / blockItemSize);
            if (tmpHeight > height * blockItemSize)
            {
                height = tmpHeight;
            }
        }
        Debug.Log($"Block {ID} width: {width}, height: {height}");
    }

    // private void Start()
    // {
    //     for (int i = 0; i < transBg.childCount; i++)
    //     {
    //         var child = transBg.GetChild(i);
    //         Debug.Log(child.transform.position - transform.position);   
    //     }
    // }
}