using UnityEngine;

namespace Gamelogic
{
    public interface IBlockEffect
    {
        void ApplyEffect(GameObject ball);
    }

    public class SolidBlockEffect : MonoBehaviour, IBlockEffect
    {
        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            Audio.AudioManager.Instance.PlayFX("HitBlock");
        }
    }

    public class FragileBlockEffect : MonoBehaviour, IBlockEffect
    {
        public float fragileDecelerate = 1.0f;
        public float breakSpeed = 5.0f;
        public int score;

        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity * (1.0f - fragileDecelerate);

            EventManager.Instance.onScoreChanged.Invoke(score);
            EventManager.Instance.onTextPoped.Invoke(score.ToString(), 1.2f, Color.yellow);
            Destroy(this.gameObject);
        }
    }

    public class SpikeBlockEffect : MonoBehaviour, IBlockEffect
    {
        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            Destroy(ball);
        }
    }

    public class StopBlockEffect : MonoBehaviour, IBlockEffect
    {
        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            Audio.AudioManager.Instance.PlayFX("StopBlock");
        }
    }

    public class LeafSpringBlockEffect : MonoBehaviour, IBlockEffect
    {
        public float force;
        public float verticalForce;

        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity.magnitude < force ? rb.velocity.normalized * force : rb.velocity;
            Vector2 dir = ball.transform.position - this.transform.position;
            Vector2 upDir = gameObject.transform.up;
            float sign = Vector2.Dot(dir, upDir);
            if (Mathf.Abs(verticalForce) > 1e-6)
            {
                if (sign > 0.0f)
                {
                    rb.velocity += upDir * verticalForce;
                }
                else
                {
                    rb.velocity -= upDir * verticalForce;
                }
            }
            //rb.velocity = rb.velocity.magnitude < force ? rb.velocity.normalized * force : rb.velocity;
            EventManager.Instance.onScoreChanged.Invoke(100);
            Audio.AudioManager.Instance.PlayFX("LeafSpring");
        }
    }

    public class TurnBlockEffect : MonoBehaviour, IBlockEffect
    {
        public Vector2 direction;
        public float force;

        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            var rb = ball.GetComponent<Rigidbody2D>();

            rb.velocity = rb.velocity.magnitude > force
                ? rb.velocity.magnitude * direction.normalized
                : force * direction.normalized;
            EventManager.Instance.onScoreChanged.Invoke(100);
        }
    }

    public class PortalBlockEffect : MonoBehaviour, IBlockEffect
    {
        public Block outBlock;

        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            if (outBlock == null)
                throw new System.Exception("PortalBlockEffect: outBlock is null");

            Debug.Log("Hit Portal Block");
            ball.transform.position = outBlock.transform.position;
            EventManager.Instance.onScoreChanged.Invoke(100);
            //var rb = ball.GetComponent<Rigidbody2D>();
            //Vector3 offset = new Vector3(rb.velocity.x, rb.velocity.y, 0.0f);
            //Vector3 bbSize = outBlock.GetComponent<BoxCollider2D>().bounds.size;
            //ball.transform.position = outBlock.transform.position + offset.normalized * 0.5f * bbSize.magnitude;
        }
    }

    public class GravitationBlockEffect : MonoBehaviour, IBlockEffect
    {
        public float gravitationStength = 10.0f;
        public float gravitationRadius = 5.0f;

        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (transform.position - ball.transform.position).normalized;
                rb.AddForce(direction * gravitationStength, ForceMode2D.Force);
            }
        }
    }

    public class ConveryorBeltEffect : MonoBehaviour, IBlockEffect
    {
        public Vector2 beltSpeed = new Vector2(1.0f, 0.0f);

        void IBlockEffect.ApplyEffect(GameObject ball)
        {
            Debug.Log("Hit Converyor Belt Block");
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity + beltSpeed;
        }

        public void RemoveEffect(GameObject ball)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity - beltSpeed;
        }
    }
}