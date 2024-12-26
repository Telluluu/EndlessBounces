using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [Header("移动")]
    public float moveUpSpeed = 2.0f;

    public Vector3 moveVector = Vector3.up;
    public const float disappearTime = 1.0f;
    private float _disappearTimer;

    private Vector3 randomDir;

    [Header("消失")]
    public float fadeDuration = 1.0f;

    private void Awake()
    {
        randomDir = Random.insideUnitCircle;
        _disappearTimer = disappearTime;
    }

    public void StartScale(AnimationCurve scaleCurve, float scaleDuration, float targetScale)
    {
        StartCoroutine(PopTextEffect(scaleCurve, scaleDuration, targetScale));
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
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator PopTextEffect(AnimationCurve scaleCurve, float scaleDuration, float targetScale)
    {
        this.transform.localScale = transform.localScale * 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            // 使用动画曲线来控制缩放速度
            float scale = Mathf.Lerp(0f, targetScale, scaleCurve.Evaluate(elapsedTime / scaleDuration));
            this.transform.localScale = new Vector3(scale, scale, 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最后的缩放为目标值
        this.transform.localScale = new Vector3(targetScale, targetScale, 1f);
    }

    private IEnumerator FadeOut()
    {
        float fadeElapsed = 0.0f;
        var tmp = this.GetComponent<TMP_Text>();
        Color startColor = tmp.color;
        while (fadeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startColor.a, 0.0f, fadeElapsed / fadeDuration);
            tmp.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            fadeElapsed += Time.deltaTime;
            yield return null;
        }
        tmp.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        Destroy(gameObject);
    }
}