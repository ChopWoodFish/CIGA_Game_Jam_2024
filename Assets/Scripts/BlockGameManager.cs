// 俄罗斯方块部分的总控制

using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockGameManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform nodeDropItem;
    public Transform genPoint;

    public float oneBlockSize = 1f;
    public float oneDropTime = 1f;
    
    private List<List<Vector3>> listMapPos = new List<List<Vector3>>();
    private List<List<bool>> listAccupation = new List<List<bool>>();
    private Block crtBlock;
    private float dropTimer;

    private void Start()
    {
        int rowNum = transform.childCount;
        int colNum = transform.GetChild(0).childCount;
        Debug.Log($"detect map row: {rowNum}, col: {colNum}");

        for (int r = 0; r < rowNum; r++)
        {
            listMapPos.Add(new List<Vector3>());
            listAccupation.Add(new List<bool>());
            for (int c = 0; c < colNum; c++)
            {
                listMapPos[r].Add(new Vector3(c * oneBlockSize + oneBlockSize / 2f, r * oneBlockSize + oneBlockSize / 2f, 0));
                listAccupation[r].Add(false);
            }
        }

        TestGenBlock();
    }

    private void TestGenBlock()
    {
        crtBlock = Instantiate(blockPrefab, nodeDropItem).GetComponent<Block>();
        crtBlock.transform.position = genPoint.position;
        crtBlock.transform.localScale = Vector3.one;
        ResetDropTimer();
    }

    private void ResetDropTimer()
    {
        dropTimer = oneDropTime;
    }

    private void Update()
    {
        if (crtBlock != null)
        {
            dropTimer -= Time.deltaTime;
            if (dropTimer <= 0)
            {
                Drop();
                ResetDropTimer();
            }
        }
    }

    private void Drop()
    {
        var pos = crtBlock.transform.position;
        pos.y -= 1;
        crtBlock.transform.position = pos;
    }
}