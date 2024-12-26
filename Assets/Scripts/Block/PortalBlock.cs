using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class PortalBlock : Block
    {
        public PortalBlock targetPortalBlock;
        private PortalBlockEffect portalBlockEffect;
        public float portalCD = 1.0f;
        public float lastPortalTime = 0.0f;

        private void OnEnable()
        {
            portalBlockEffect = gameObject.AddComponent<PortalBlockEffect>();
            base.blockEffect = portalBlockEffect;

            portalBlockEffect.outBlock = targetPortalBlock;

            lastPortalTime = Time.time - portalCD - 1.0f;
        }

        private void Update()
        {
        }

        protected new void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Ball")
            {
                if (Time.time - lastPortalTime > portalCD)
                {
                    blockEffect?.ApplyEffect(collider.gameObject);
                    lastPortalTime = Time.time;
                    targetPortalBlock.lastPortalTime = lastPortalTime;
                }
            }
        }
    }
}