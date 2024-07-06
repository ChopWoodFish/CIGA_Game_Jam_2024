using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private Vector3 startPosition;
    private GameObject player;

    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.enabled = false;
            }
            StartCoroutine(PlayerDieAndRespawn(collision.gameObject));
        }
    }

    IEnumerator PlayerDieAndRespawn(GameObject player)
    {
        // 玩家死亡处理，例如播放死亡动画等
        // ...

        // 假设死亡动画播放需要2秒钟
        yield return new WaitForSeconds(2);

        // 复活玩家在起始位置
        player.transform.position = startPosition;

        // 重新启用PlayerMove组件
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.enabled = true;
        }
    }
}

