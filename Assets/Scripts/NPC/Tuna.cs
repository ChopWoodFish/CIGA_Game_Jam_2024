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
            Debug.Log("����Ѿ���ȡ�˹�ͷ��");
        }

    }

    void getTuna()
    {

        hasTuna = true;
        IntEventSystem.Send(GameEventEnum.GetTuna, null);

        // ����������Ӹ����߼������������ϷUI�����ӷ�����

        gameObject.SetActive(false);
        
    }
}
