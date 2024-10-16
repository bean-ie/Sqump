using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] PostProcessingController ppController;
    [SerializeField] ScoreController scoreController;
    [SerializeField] GameObject deathParticle;
    [SerializeField] PlayerSkinManager skinManager;
    [SerializeField] AudioSource deathSound, winSound;
    public bool immune;
    Transform finishPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            PlayerPrefs.SetInt(LevelInfoHolder.instance.chosenLevel.levelName + "wins", PlayerPrefs.GetInt(LevelInfoHolder.instance.chosenLevel.levelName + "wins") + 1);
            collision.enabled = false;
            StartCoroutine("ResetLevel");
            finishPortal = collision.transform;
            StartCoroutine("GoIntoPortal");
            scoreController.AddScore(1);
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (immune) return;
        PlayerPrefs.SetInt(LevelInfoHolder.instance.chosenLevel.levelName + "deaths", PlayerPrefs.GetInt(LevelInfoHolder.instance.chosenLevel.levelName + "deaths") + 1);
        deathSound.Play();
        GameObject particle = Instantiate(deathParticle, transform);
        particle.GetComponent<ParticleSystemRenderer>().material.SetTexture("_EmissionMap", skinManager.GetCurrentSkin().texture);
        particle.GetComponent<ParticleSystemRenderer>().material.mainTexture = skinManager.GetCurrentSkin().texture;
        Destroy(particle, 3f);
        StartCoroutine("ResetLevel");
        StartCoroutine("HidePlayer");
        scoreController.ResetScore();
    }

    IEnumerator ResetLevel()
    {
        immune = true;
        Color baseColor = fadeImage.color;
        for (float alpha = 0f; alpha <= 1f; alpha += Time.deltaTime * fadeSpeed)
        {
            if (alpha > 0.4f)
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            baseColor.a = alpha;
            fadeImage.color = baseColor;
            yield return null;
        }
        baseColor.a = 1;
        fadeImage.color = baseColor;
        transform.position = Vector2.zero;
        levelGenerator.RegenerateLevel();
        ppController.RandomizeEverything();
        yield return new WaitForSeconds(0.5f);
        for (float alpha = 1f; alpha >= 0f; alpha -= Time.deltaTime * fadeSpeed)
        {
            if (alpha < 0.6f)
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            baseColor.a = alpha;
            fadeImage.color = baseColor;
            yield return null;
        }
        baseColor.a = 0;
        fadeImage.color = baseColor;
        transform.localScale = Vector2.one;
        immune = false;
    }

    IEnumerator GoIntoPortal()
    {
        winSound.Play();
        Vector3 directionFromPortal = transform.position - finishPortal.position;
        for (float scale = transform.localScale.x; scale >= 0; scale -= Time.deltaTime / 0.5f)
        {
            transform.position = finishPortal.position + directionFromPortal * scale;
            transform.localScale = Vector2.one * scale;
            yield return null;
        }
        transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        for (float scale = transform.localScale.x; scale <= 1; scale += Time.deltaTime / 0.5f)
        {
            transform.localScale = Vector2.one * scale;
            yield return null;
        }
        transform.localScale = Vector2.one;
    }

    IEnumerator HidePlayer()
    {
        TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer);
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(1f);

        spriteRenderer.enabled = true;
    }
}
