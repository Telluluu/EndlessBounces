using Gamelogic;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gamelogic
{
    public class TurnBlock : Block
    {
        public Vector2 direction;

        public float force;

        private void OnEnable()
        {
            base.blockType = BlockType.Interactable;
            var turnBlockEffect = gameObject.AddComponent<TurnBlockEffect>();
            turnBlockEffect.direction = transform.right;
            turnBlockEffect.force = force;
            base.blockEffect = turnBlockEffect;
        }

        private new void OnTriggerEnter2D(Collider2D collider)
        {
            base.blockEffect.ApplyEffect(collider.gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            // Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * 5.0f);
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 5.0f);
        }
    }
}