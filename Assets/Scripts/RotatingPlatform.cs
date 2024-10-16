using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float rotationSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.rotation = Random.Range(0f, 360f);
        rotationSpeed *= Random.Range(0.9f, 1.1f);
    }

    private void Update()
    {
        rb.rotation += Time.deltaTime * rotationSpeed;
    }
}
