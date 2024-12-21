using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockEffect
{
    void ApplyEffect(GameObject ball);
}

public class SolidBlockEffect : MonoBehaviour, IBlockEffect
{
    void IBlockEffect.ApplyEffect(GameObject ball)
    {
        //Debug.Log("Hit Solid");
    }
}

public class FragileBlockEffect : MonoBehaviour, IBlockEffect
{
    void IBlockEffect.ApplyEffect(GameObject ball)
    {
        Debug.Log("Hit Fragile Block");
        var rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = rb.velocity * 0.5f;
        Destroy(this.gameObject);
    }
}

public class SpikeBlockEffect : MonoBehaviour, IBlockEffect
{
    void IBlockEffect.ApplyEffect(GameObject ball)
    {
        Debug.Log("Hit Spike Block");
        Destroy(ball);
    }
}

public class StopBlockEffect : MonoBehaviour, IBlockEffect
{
    void IBlockEffect.ApplyEffect(GameObject ball)
    {
        Debug.Log("Hit Stop Block");
        var rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }
}

public class LeafSpringBlockEffect : MonoBehaviour, IBlockEffect
{
    void IBlockEffect.ApplyEffect(GameObject ball)
    {
        Debug.Log("Hit Leaf Spring Block");
        var rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = rb.velocity * 1.5f;
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
        Debug.Log(ball.name + " Enter Gravitation Block Range");

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (transform.position - ball.transform.position).normalized;
            rb.AddForce(direction * gravitationStength, ForceMode2D.Force);
        }
    }
}