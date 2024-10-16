using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.velocity += Vector2.up * 20;
            collision.gameObject.GetComponent<PlayerMovement>().PlayJumpSound();
        }
    }
}
