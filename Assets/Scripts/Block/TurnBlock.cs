using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class TurnBlock : Block
    {
        public Vector2 direction;

        public float force;

        private TurnBlockEffect _turnBlockEffect;

        private void OnEnable()
        {
            base.blockType = BlockType.Interactable;
            _turnBlockEffect = gameObject.AddComponent<TurnBlockEffect>();
            base.blockEffect = _turnBlockEffect;
        }

        private void Update()
        {
            _turnBlockEffect.direction = transform.right;
            _turnBlockEffect.force = force;
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