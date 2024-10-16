using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePlatform : MonoBehaviour
{
    [SerializeField] float speed, delayBeforeReturn;
    Rigidbody2D rb;
    public float distance = 10;
    public float direction = 1;
    Vector2 startPosition;
    Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        for (float move = 0; move < distance; move += Time.deltaTime * speed)
        {
            transform.position = startPosition + Vector2.right * move * direction;
            if (player != null) player.position += Vector3.right * Time.deltaTime * speed * direction;
            yield return null;
        }
        transform.position = startPosition + Vector2.right * distance * direction;
        yield return new WaitForSeconds(delayBeforeReturn);

        for (float move = distance; move > 0; move -= Time.deltaTime * speed)
        {
            transform.position = startPosition + Vector2.right * move * direction;
            if (player != null) player.position -= Vector3.right * Time.deltaTime * speed * direction;
            yield return null;
        }
        transform.position = startPosition;
        yield return new WaitForSeconds(delayBeforeReturn);

        StartCoroutine("Move");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
