using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class LeafSpringBlock : Block
    {
        private LeafSpringBlockEffect leafSpringBlockEffect;
        public float force = 50.0f;

        private void OnEnable()
        {
            base.blockType = BlockType.Interactable;
            leafSpringBlockEffect = gameObject.AddComponent<LeafSpringBlockEffect>();
            leafSpringBlockEffect.force = force;
            base.blockEffect = leafSpringBlockEffect;
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                base.blockEffect.ApplyEffect(collision.gameObject);
            }
        }
    }
}