using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuna : MonoBehaviour
{

    public bool hasTuna = false;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaytunaSound();
            getTuna();
            Debug.Log("玩家已经获取了罐头！");
        }

    }

    void getTuna()
    {

        hasTuna = true;

        // 这里可以添加更多逻辑，比如更新游戏UI，增加分数等

        gameObject.SetActive(false);
        
    }
}
