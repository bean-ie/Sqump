using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    SpringJoint2D springJoint;

    private void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            springJoint.dampingRatio = 0;
            springJoint.frequency = 1;
        }
    }
}
