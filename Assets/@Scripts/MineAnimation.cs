using UnityEngine;
using DG.Tweening;

public class MineAnimation : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;  // 흔들림이 지속되는 시간
    [SerializeField] private float shakeStrength = 0.1f;  // 흔들림의 강도
    [SerializeField] private int shakeVibrato = 10;       // 진동 수
    [SerializeField] private float shakeRandomness = 90f; // 무작위성 정도

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        
        
    }

    public void ShakeOnce()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, false, true)
            .OnComplete(() => transform.position = originalPosition);
    }
}