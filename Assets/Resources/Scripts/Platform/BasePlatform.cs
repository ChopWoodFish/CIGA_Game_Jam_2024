
using System;
using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            anim.Play("Cloud_Bounce");
        }
    }
}