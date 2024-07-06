using UnityEngine;

public class BlockItem : MonoBehaviour
{
    public Transform transOutline;
    public int typeID;

    public void SetOutlineEnable(bool isEnable)
    {
        transOutline.gameObject.SetActive(isEnable);
    }
}