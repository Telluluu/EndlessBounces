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
        if (Mathf.Abs(rb.velocity.y) > 3.0f)
        {
            animator.SetInteger("FallLevel", 1);
            if (Mathf.Abs(rb.velocity.y) > 10.0f)
                animator.SetInteger("FallLevel", 2);
            if (Mathf.Abs(rb.velocity.y) > 20.0f)
                animator.SetInteger("FallLevel", 3);
        }
        else
        {
            animator.SetInteger("FallLevel", 0);
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

            // 随机碰撞效果
            int randomInteger = Random.Range(0, 3);
            switch (randomInteger)
            {
                case 0:
                    animator.SetTrigger("Touch");
                    break;

                case 1:
                    animator.SetTrigger("Touch2");
                    break;

                case 2:
                    animator.SetTrigger("Touch3");
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var dir = collision.transform.position - transform.position;

        if ((rb.velocity * dir.normalized).magnitude > 2.0f)
        {
            Vector2 direction = (Vector2)collision.transform.position - (Vector2)transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // 随机碰撞效果
            int randomInteger = Random.Range(0, 3);
            switch (randomInteger)
            {
                case 0:
                    animator.SetTrigger("Touch");
                    break;

                case 1:
                    animator.SetTrigger("Touch2");
                    break;

                case 2:
                    animator.SetTrigger("Touch3");
                    break;
            }
        }
    }
}