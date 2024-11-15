using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    public float shakeDuration = 0.5f;  // 흔들림이 지속되는 시간
    public float shakeStrength = 0.1f;  // 흔들림의 강도
    public int shakeVibrato = 10;       // 진동 수
    public float shakeRandomness = 90f; // 무작위성 정도

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        ShakeOnce();
        
        
    }

    private void ShakeOnce()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, false, true)
            .OnComplete(() => transform.position = originalPosition);
    }
}
