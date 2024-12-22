using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    public float speedLimit;
    public float decelerationRate = 5.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        }
        if (rb.velocity.magnitude > speedLimit)
        {
            Vector2 targetVelocity = rb.velocity.normalized * speedLimit;

            // 平滑地将当前速度移动向目标速度
            //rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, decelerationRate * Time.deltaTime);
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }
}