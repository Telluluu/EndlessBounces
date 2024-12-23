using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (rb.velocity.y < -0.1f || rb.velocity.y > 0.1f)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var dir = collision.transform.position - transform.position;

        if ((rb.velocity * dir.normalized).magnitude > 2.0f)
        {
            Vector2 direction = collision.contacts[0].point - (Vector2)transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            animator.SetTrigger("Touch");
        }
    }
}