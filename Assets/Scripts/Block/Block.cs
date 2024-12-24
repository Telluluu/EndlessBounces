using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gamelogic
{
    public class Block : MonoBehaviour
    {
        public enum BlockType
        {
            Solid,
            Fragile,
            Spike,
            Stop,
            Interactable
        }

        public int blockScore = 50;

        public BlockType blockType;

        public IBlockEffect blockEffect;

        private void OnEnable()
        {
            switch (blockType)
            {
                case BlockType.Solid:

                    blockEffect = gameObject.AddComponent<SolidBlockEffect>();
                    break;

                case BlockType.Fragile:
                    blockEffect = gameObject.AddComponent<FragileBlockEffect>();
                    break;

                case BlockType.Spike:
                    blockEffect = gameObject.AddComponent<SpikeBlockEffect>();
                    break;

                case BlockType.Stop:
                    blockEffect = gameObject.AddComponent<StopBlockEffect>();
                    break;

                case BlockType.Interactable:
                    blockEffect = null;
                    break;
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                blockEffect?.ApplyEffect(collision.gameObject);
            }
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                blockEffect?.ApplyEffect(collision.gameObject);
            }
        }
    }
}