using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Gamelogic
{
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

        private void FixedUpdate()
        {
            //if (Keyboard.current.aKey.isPressed)
            //{
            //    rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
            //}
            //if (Keyboard.current.dKey.isPressed)
            //{
            //    rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            //}
            //if (rb.velocity.magnitude > speedLimit)
            //{
            //    // 平滑地将当前速度移动向目标速度
            //    //Vector2 targetVelocity = rb.velocity.normalized * speedLimit;
            //    //rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, decelerationRate * Time.deltaTime);
            //    rb.velocity = rb.velocity.normalized * speedLimit;
            //}
        }

        public void Launch(Vector2 dir, float speed)
        {
            rb.velocity = dir.normalized * speed;
        }
    }
}