using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] Transform cam;
    void Update()
    {
        transform.position = Vector3.right * cam.position.x + Vector3.up * transform.position.y + -Vector3.forward;
    }
}
