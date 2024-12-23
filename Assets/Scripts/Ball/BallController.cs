using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

namespace Gamelogic
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody2D rb;
        public float force;
        public float speedLimit;
        public float decelerationRate = 5.0f;
        public float lastCollisionTime;
        public float comboInterval = 1.0f;
        public int combo = 0;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
        }

        private void FixedUpdate()
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
                // 平滑地将当前速度移动向目标速度
                //Vector2 targetVelocity = rb.velocity.normalized * speedLimit;
                //rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, decelerationRate * Time.deltaTime);
                rb.velocity = rb.velocity.normalized * speedLimit;
            }
        }

        public void Launch(Vector2 dir, float speed)
        {
            rb.velocity = dir.normalized * speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var dir = other.transform.position - transform.position;
            var speedInDir = Vector2.Dot(rb.velocity, dir.normalized);
            Block block = other.gameObject.GetComponent<Block>();
            if (block == null || block.blockType != Block.BlockType.Interactable)
            {
                if (speedInDir < 2.0f)
                    return;
                else
                {
                    EventManager.Instance.onScoreChanged.Invoke(50);
                    Debug.Log(speedInDir);
                }
            }

            lastCollisionTime = Time.time;
            if (Time.time - lastCollisionTime > comboInterval)
            {
                combo = 1;
            }
            else
            {
                combo++;
                if (combo >= 3)
                {
                    GameManager.Instance.ComboMagnificate();
                }
            }
        }
    }
}