using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public float disappearDelay = 2.0f; // 玩家站上去后消失前的延迟时间
    public float reappearDelay = 2.0f; // 消失后重新出现的延迟时间

    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    private bool playerOnPlatform;

    void Start()
    {
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
        //其他逻辑，比如播放动画
        //...
        platformCollider.enabled = false;
        platformRenderer.enabled = false;
        Invoke("ReappearPlatform", reappearDelay);
    }

    void ReappearPlatform()
    {
        platformCollider.enabled = true;
        platformRenderer.enabled = true;
    }
}
