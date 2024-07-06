// 俄罗斯方块部分的总控制

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockGameManager : MonoBehaviour
{
    public Transform nodeBg;
    
    // public GameObject blockPrefab;
    private BlockPicker blockPicker;
    public Transform nodeDropItem;

    public float oneBlockSize = 1f;
    public float oneDropTime = 1f;
    
    private List<List<Vector3>> listMapPos = new List<List<Vector3>>();
    private List<List<bool>> listAccupation = new List<List<bool>>();
    private Vector2Int crtBlockLeftBottomPos;
    private Block crtBlock;
    private float dropTimer;

    private void Awake()
    {
        blockPicker = new BlockPicker();
        blockPicker.CollectPrefab();
    }

    private void Start()
    {
        int rowNum = nodeBg.childCount;
        int colNum = nodeBg.GetChild(0).childCount;
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
        var blockPrefab = blockPicker.SelectRandomBlock();
        crtBlock = Instantiate(blockPrefab,nodeDropItem).GetComponent<Block>();
        Vector2Int mapPos = SelectGenPos();
        crtBlock.transform.localScale = Vector3.one;
        crtBlock.transform.position = transform.position + listMapPos[mapPos.x][mapPos.y];

        crtBlockLeftBottomPos = mapPos;
        
        ResetDropTimer();
    }

    private const int GenRowIndex = 8;
    private Vector2Int SelectGenPos()
    {
        var genRow = listMapPos[^1];
        int maxCol = genRow.Count - crtBlock.width - 1;
        int genPosCol = Random.Range(0, maxCol);

        Vector2Int pos = new Vector2Int() {x = GenRowIndex, y = genPosCol};
        Debug.Log($"Gen Pos Max Col: {maxCol}, Col: {genPosCol}, pos {pos}");
        return pos;
    }

    private void ResetDropTimer()
    {
        dropTimer = oneDropTime;
    }

    private void Update()
    {
        if (crtBlock == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            bool canMove = CheckCanMoveHorizontally(true);
            if (canMove)
            {
                MoveHorizontally(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            bool canMove = CheckCanMoveHorizontally(false);
            if (canMove)
            {
                MoveHorizontally(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ForceDrop();
        }

        dropTimer -= Time.deltaTime;
        if (dropTimer <= 0)
        {
            if (CheckCanDrop())
            {
                Drop();
                ResetDropTimer();
            }
            else
            {
                SettleDown();
                TestGenBlock();
                ResetDropTimer();
            }
        }
        
        
    }

    private bool CheckCanMoveHorizontally(bool isLeft)
    {
        Vector2Int newLBPos;
        // Debug.Log($"Check with LB: {crtBlockLeftBottomPos}");
        if (isLeft)
        {
            if (crtBlockLeftBottomPos.y == 0)
            {
                return false;
            }
            newLBPos = crtBlockLeftBottomPos + new Vector2Int(0, -1);
        }
        else
        {
            if (crtBlockLeftBottomPos.y == listMapPos[0].Count - 1)
            {
                return false;
            }
            newLBPos = crtBlockLeftBottomPos + new Vector2Int(0, 1);
        }
        
        // Debug.Log($"Check with new LB: {newLBPos}");
        for (int i = 0; i < crtBlock.localPosOffset.Count; i++)
        {
            var offset = crtBlock.localPosOffset[i];
            Vector2Int tmpNewPos = newLBPos + crtBlock.localPosOffset[i];
            // Debug.Log($"Check with new LB: {newLBPos}, offset: {offset}, tmpPos: {tmpNewPos}");
            if (tmpNewPos.x < 0 || tmpNewPos.x >= listMapPos.Count || tmpNewPos.y < 0 ||
                tmpNewPos.y >= listMapPos[0].Count)
            {
                return false;
            }
            bool isOccupied = listAccupation[tmpNewPos.x][tmpNewPos.y];
            if (isOccupied)
            {
                return false;
            }
        }

        return true;
    }

    private void MoveHorizontally(bool isLeft)
    {
        crtBlockLeftBottomPos += new Vector2Int(0, isLeft ? -1 : 1);
        crtBlock.transform.position += new Vector3(isLeft ? -oneBlockSize : oneBlockSize, 0, 0);
    }

    private bool CheckCanDrop()
    {
        if (crtBlockLeftBottomPos.x == 0)
        {
            return false;
        }
        
        var newLBPos = crtBlockLeftBottomPos + new Vector2Int(-1, 0);
        for (int i = 0; i < crtBlock.localPosOffset.Count; i++)
        {
            var offset = crtBlock.localPosOffset[i];
            Vector2Int tmpNewPos = newLBPos + crtBlock.localPosOffset[i];
            // Debug.Log($"Check with new LB: {newLBPos}, offset: {offset}, tmpPos: {tmpNewPos}");
            if (tmpNewPos.x < 0 || tmpNewPos.x >= listMapPos.Count || tmpNewPos.y < 0 ||
                tmpNewPos.y >= listMapPos[0].Count)
            {
                return false;
            }
            bool isOccupied = listAccupation[tmpNewPos.x][tmpNewPos.y];
            if (isOccupied)
            {
                return false;
            }
        }

        return true;
    }

    private void Drop()
    {
        var pos = crtBlock.transform.position;
        pos.y -= 1;
        crtBlock.transform.position = pos;
        crtBlockLeftBottomPos += new Vector2Int(-1, 0);
    }

    private void ForceDrop()
    {
        int crtRow = crtBlockLeftBottomPos.x;
        int dropDist = 1;
        while (crtRow - dropDist >= 0)
        {
            bool canDrop = CheckCanDrop();
            if (canDrop)
            {
                Drop();
                dropDist++;
            }
            else
            {
                SettleDown();
                ResetDropTimer();
                return;
            }
        }
    }

    private void SettleDown()
    {
        Debug.Log($"settle down block {crtBlock.ID} at LB pos: {crtBlockLeftBottomPos}");
        for (int i = 0; i < crtBlock.localPosOffset.Count; i++)
        {
            var offset = crtBlock.localPosOffset[i];
            Vector2Int tmpNewPos = crtBlockLeftBottomPos + offset;
            listAccupation[tmpNewPos.x][tmpNewPos.y] = true;
            Debug.Log($"====occupy {tmpNewPos}");
        }
    }
}