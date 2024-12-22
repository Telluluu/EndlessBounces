using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpringBlock : Block
{
    private LeafSpringBlockEffect leafSpringBlockEffect;

    private void OnEnable()
    {
        base.blockType = BlockType.Interactable;
        leafSpringBlockEffect = gameObject.AddComponent<LeafSpringBlockEffect>();
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