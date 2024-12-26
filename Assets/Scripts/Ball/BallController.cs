using UnityEngine;

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
        public bool isStopped = false;
        public bool isLaunched = false;

        public float getScoreSpeed = 4.0f;

        private float stopTime = 0.0f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }

        private void Update()
        {
            if (isLaunched == false)
                rb.velocity = Vector2.zero;
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
            if (rb.velocity.magnitude > speedLimit)
            {
                // 平滑地将当前速度移动向目标速度
                //Vector2 targetVelocity = rb.velocity.normalized * speedLimit;
                //rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, decelerationRate * Time.deltaTime);
                rb.velocity = rb.velocity.normalized * speedLimit;
            }
            else if (isLaunched && rb.velocity.magnitude < 0.1f)
            {
                stopTime += Time.deltaTime;
            }
            if (stopTime > 1.0f)
            {
                rb.velocity = Vector2.zero;
                isStopped = true;
            }
        }

        public void Launch(Vector2 dir, float speed)
        {
            isLaunched = true;
            rb.isKinematic = false;
            rb.velocity = dir.normalized * speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Block block = other.gameObject.GetComponent<Block>();
            if (block == null)
                return;

            var dir = other.contacts[0].normal;
            var speedInDir = Vector2.Dot(rb.velocity, dir.normalized);

            if (block.blockType == Block.BlockType.Solid)
            {
                if (speedInDir < getScoreSpeed)
                    return;
                else
                {
                    EventManager.Instance.onScoreChanged.Invoke(block.blockScore);
                    // EventManager.Instance.onTextPoped.Invoke(block.blockScore.ToString(), 1.0f + combo / 3.0f);
                }
            }
            else if (block.blockType == Block.BlockType.Fragile)
            {
                EventManager.Instance.onScoreChanged.Invoke(block.blockScore);
            }
            else if (block.blockType == Block.BlockType.Stop)
            {
                combo = 0;
                lastCollisionTime = Time.time;
                return;
            }

            lastCollisionTime = Time.time;
            if (Time.time - lastCollisionTime > comboInterval)
            {
                combo = 1;
                EventManager.Instance.onTextPoped.Invoke(block.blockScore.ToString(), 1.0f, Color.white);
            }
            else
            {
                combo++;
                EventManager.Instance.onTextPoped.Invoke(block.blockScore.ToString(), 1.0f + combo / 3.0f, Color.white);
                if (combo >= 3)
                {
                    GameManager.Instance.ComboMagnificate();
                    combo = 0;
                }
            }
        }
    }
}