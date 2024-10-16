using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] float disappearDelay, reappearTime, disappearedAlpha = 0.5f, disappearTime;
    Color disappearColor, normalColor;
    SpriteRenderer sprite;
    Collider2D collider;
    bool disappearing = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !disappearing)
        {
            disappearing = true;
            StartCoroutine("Disappear");
        }
    }

    IEnumerator Disappear()
    {
        normalColor = sprite.color;
        disappearColor = sprite.color;
        yield return new WaitForSeconds(disappearDelay);
        for (float alpha = 1f; alpha >= disappearedAlpha; alpha -= Time.deltaTime / disappearTime * (1 - disappearedAlpha))
        {
            disappearColor.a = alpha;
            sprite.color = disappearColor;
            yield return null;
        }
        disappearColor.a = disappearedAlpha;
        sprite.color = disappearColor;
        collider.enabled = false;
        yield return new WaitForSeconds(reappearTime);
        for (float alpha = disappearedAlpha; alpha <= 1f; alpha += Time.deltaTime / disappearTime * (1 - disappearedAlpha))
        {
            disappearColor.a = alpha;
            sprite.color = disappearColor;
            yield return null;
        }
        sprite.color = normalColor;
        collider.enabled = true;
        disappearing = false;
    }
}
