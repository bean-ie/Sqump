using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speedModifier = 1;
    [SerializeField] float speed, jumpSpeed;
    bool grounded = false;
    Vector2 movement;
    Vector2 velocity = Vector2.zero;
    float dashCooldown;
    int doubleJumpsLeft;
    [SerializeField] float doubleJumpDelayMax;
    float doubleJumpDelayTimer;
    [SerializeField] GameObject dashParticle;
    [SerializeField] AudioSource jumpSound;
    PlayerManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        velocity.x = movement.x * speed * speedModifier;
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            velocity.y = rb.velocity.y + jumpSpeed;
            doubleJumpDelayTimer = doubleJumpDelayMax;

            PlayJumpSound();
        } 
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJumpsLeft > 0 && doubleJumpDelayTimer <= 0)
        {
            velocity.y = rb.velocity.y + jumpSpeed;
            doubleJumpsLeft--;
        }
        else 
        {
            velocity.y = rb.velocity.y;
        }

        /*if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0)
        {
            Destroy(Instantiate(dashParticle, transform.position, Quaternion.identity), 0.5f);
            rb.AddForce(10000 * movement);
            Destroy(Instantiate(dashParticle, transform.position, Quaternion.identity), 0.5f);
        }*/

        rb.velocity = velocity;

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            grounded = false;
        }

        if (doubleJumpDelayTimer > 0)
        {
            doubleJumpDelayTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        if (!collision.gameObject.TryGetComponent<SpeedPlatform>(out SpeedPlatform sp))
        {
            speedModifier = 1;
        }
        doubleJumpsLeft = LevelInfoHolder.instance.chosenLevel.doubleJumpAmount;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    public void AddVerticalSpeed(float speed)
    {
        rb.velocity += Vector2.up * speed;
    }

    public void PlayJumpSound()
    {
        if (!manager.immune)
        {
            jumpSound.pitch = Random.Range(0.9f, 1.2f);
            jumpSound.Play();
        }
    }
}
