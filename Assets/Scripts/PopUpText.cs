using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [Header("移动")]
    public float moveUpSpeed = 2.0f;

    public Vector3 moveVector = Vector3.up;
    public const float disappearTime = 1.0f;
    private float _disappearTimer;

    private Vector3 randomDir;

    private void Awake()
    {
        randomDir = Random.insideUnitCircle;
        _disappearTimer = disappearTime;
    }

    private void Update()
    {
        // 字体移动
        transform.position += randomDir * Time.deltaTime * moveUpSpeed;
        moveVector += moveUpSpeed * Time.deltaTime * randomDir;
        // 字体消失
        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}