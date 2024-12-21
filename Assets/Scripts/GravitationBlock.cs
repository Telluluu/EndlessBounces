using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class GravitationBlock : Block
    {
        private GravitationBlockEffect gravitationBlockEffect;
        public float gravitationRadius = 10.0f;
        public float gravitationStength = 12.0f;

        private void OnEnable()
        {
            gravitationBlockEffect = gameObject.AddComponent<GravitationBlockEffect>();
            base.blockEffect = gravitationBlockEffect;
        }

        private void FixedUpdate()
        {
            gravitationBlockEffect.gravitationRadius = gravitationRadius;
            gravitationBlockEffect.gravitationStength = gravitationStength;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravitationRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                    blockEffect.ApplyEffect(collider.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, gravitationRadius);
        }
    }
}