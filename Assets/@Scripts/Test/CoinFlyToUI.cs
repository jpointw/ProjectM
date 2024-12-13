using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CoinFlyToUI : MonoBehaviour
{
    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private RectTransform TargetPosition;
    

    private Vector2[] initialPos;
    private Quaternion[] initialRotation;


    private int coinsAmount;

    void Start()
    {
        coinsAmount = pileOfCoins.transform.childCount;

        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < coinsAmount; i++)
        {
            GameObject temp = pileOfCoins.transform.GetChild(i).gameObject;
            initialPos[i] = temp.GetComponent<RectTransform>().anchoredPosition;
            initialRotation[i] = temp.GetComponent<RectTransform>().rotation;
            temp.transform.localScale = Vector3.zero;


        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CoinParty();
        }
    }

    public void CoinParty()
    {
        pileOfCoins.SetActive(true);
        var delay = 0f;

        for (int i = 0; i < coinsAmount; i++)
        {
            pileOfCoins.transform.GetChild(i).gameObject.SetActive(true);
            pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(TargetPosition.localPosition, 0.8f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);


            pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash);


            pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;}

        StartCoroutine(InitCoin());
        
    }
    
    IEnumerator InitCoin()
    {
        yield return new WaitForSecondsRealtime(2f);

        StartCoroutine(Count(100, 0, 2f));

        for (int i = 0; i < coinsAmount; i++)
        {
            Transform temp = pileOfCoins.transform.GetChild(i);

            temp.GetComponent<RectTransform>().anchoredPosition = initialPos[i];
            temp.GetComponent<RectTransform>().rotation = initialRotation[i];

            temp.gameObject.SetActive(false);
        }

        pileOfCoins.SetActive(false);
    }
    
    IEnumerator Count(float target, float current, float duration)
    {
        float offset = (target - current) / duration;
        while (current < target)
        {
            current += offset * Time.deltaTime;
            yield return null;

        }
        current = target;
    }
}
