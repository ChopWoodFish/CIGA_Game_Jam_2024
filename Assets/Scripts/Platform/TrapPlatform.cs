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
            // StartCoroutine(PlayerDieAndRespawn(collision.gameObject));
            IntEventSystem.Send(GameEventEnum.PlayerRespawn, collision.gameObject);
        }
    }

    IEnumerator PlayerDieAndRespawn(GameObject player)
    {
        // ��������������粥������������
        // ...

        // ������������������Ҫ2����
        yield return new WaitForSeconds(2);

        // �����������ʼλ��
        player.transform.position = startPosition;

        // ��������PlayerMove���
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.enabled = true;
        }
    }
}

