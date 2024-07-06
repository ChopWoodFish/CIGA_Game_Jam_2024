using UnityEngine;

public class PassTransport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IntEventSystem.Send(GameEventEnum.GoToNextMap, null);
        }
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         IntEventSystem.Send(GameEventEnum.GoToNextMap, null);
    //     }
    // }
}

