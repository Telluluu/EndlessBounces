using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class ConveyorBelt : Block
    {
        public Vector2 beltSpeed = new Vector2(1.0f, 0.0f);
        private ConveryorBeltEffect converyorBeltEffect;

        private void OnEnable()
        {
            converyorBeltEffect = gameObject.AddComponent<ConveryorBeltEffect>();
            base.blockEffect = converyorBeltEffect;
            converyorBeltEffect.beltSpeed = beltSpeed;
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                blockEffect?.ApplyEffect(collision.gameObject);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                converyorBeltEffect?.RemoveEffect(collision.gameObject);
            }
        }
    }
}