using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlatform : MonoBehaviour
{
    [SerializeField]
    float switchDelay, timeToRotate;
    //int rotateDirection;
    Rigidbody2D rb;
    private void Start()
    {
        //rotateDirection = (int)Mathf.Sign(Random.value - 0.5f);
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("BeginCycle");
    }

    IEnumerator BeginCycle()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        StartCoroutine("RotateCycle");
    }

    IEnumerator RotateCycle()
    {
        yield return new WaitForSeconds(switchDelay);
        for (float rotation = 0; rotation <= 180/* * rotateDirection*/; rotation += ((180 * Time.deltaTime) / timeToRotate)/* * rotateDirection*/)
        {
            rb.rotation = rotation;
            yield return null;
        }
        rb.rotation = 180;
        yield return new WaitForSeconds(switchDelay);
        for (float rotation = 180; rotation >= 0; rotation -= ((180 * Time.deltaTime) / timeToRotate)/* * rotateDirection*/)
        {
            rb.rotation = rotation;
            yield return null;
        }
        rb.rotation = 0;
        StartCoroutine("RotateCycle");
    }
}
