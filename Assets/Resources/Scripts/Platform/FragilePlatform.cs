using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public float disappearDelay = 2.0f; // ���վ��ȥ����ʧǰ���ӳ�ʱ��
    public float reappearDelay = 2.0f; // ��ʧ�����³��ֵ��ӳ�ʱ��

    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    private bool playerOnPlatform;
    private Animator anim;

    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 center = platformCollider.bounds.center;

        if (collision.collider.CompareTag("Player") && contactPoint.y > center.y)
        {
            Invoke("DisappearPlatform", disappearDelay);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    void DisappearPlatform()
    {
        //�����߼������粥�Ŷ���
        //...
        platformCollider.enabled = false;
        // platformRenderer.enabled = false;
        Invoke("ReappearPlatform", reappearDelay);
        anim.Play("Break");
    }

    void ReappearPlatform()
    {
        platformCollider.enabled = true;
        // platformRenderer.enabled = true;
        anim.Play("Restore");
    }
}
