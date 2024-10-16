using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealColorPlatform : MonoBehaviour
{
    [SerializeField] Color revealColor;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = revealColor;
        }
    }
}
