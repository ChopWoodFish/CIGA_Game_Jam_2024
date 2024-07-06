using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewSlot : MonoBehaviour
{
    private List<Image> listImgGrid = new List<Image>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            listImgGrid.Add(transform.GetChild(i).GetComponent<Image>());
        }
    }

    public void SetBlockPreview(GameObject blockGO)
    {
        // Debug.Log($"block preview {blockGO.transform.GetChild(0).childCount}");
        Debug.Log($"preview {blockGO.name}");

        foreach (var imgGrid in listImgGrid)
        {
            imgGrid.enabled = false;
        }

        for (int i = 0; i < blockGO.transform.GetChild(0).childCount; i++)
        {
            var child = blockGO.transform.GetChild(0).GetChild(i);
            int index = (int) child.transform.position.y * 3 + (int) child.transform.position.x;
            listImgGrid[index].enabled = true;
        }
    }
}