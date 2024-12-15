using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;
using static Define;

public class TitleScene : SceneBase
{
    
    public Image gameLogoImage;
    public Button gameStartButton;
    
    public GameModelDatas gameModelDatas;
    protected override bool Init()
    {
        return base.Init();
    }

    public void Start()
    {
        GameSystems.Instance.Init();
        StartShakeAnimation();
        gameStartButton.onClick.AddListener(() => GameSystems.Scene.ChangeScene(SceneType.GameScene));
        GameSystems.Instance.gameModelDatas = gameModelDatas;
    }

    private void StartShakeAnimation()
    {
        Sequence explosionSequence = DOTween.Sequence();

        explosionSequence.Append(gameLogoImage.rectTransform.DOLocalRotate(new Vector3(0, 0, 10), 0.1f)
                .SetEase(Ease.OutBack))
            .Append(gameLogoImage.rectTransform.DOLocalRotate(new Vector3(0, 0, -10), 0.1f)
                .SetEase(Ease.OutBack))
            .Append(gameLogoImage.rectTransform.DOLocalRotate(new Vector3(0, 0, 10), 0.08f)
                .SetEase(Ease.OutBack))
            .Append(gameLogoImage.rectTransform.DOLocalRotate(new Vector3(0, 0, -10), 0.08f)
                .SetEase(Ease.OutBack))
            .Append(gameLogoImage.rectTransform.DOLocalRotate(Vector3.zero, 0.05f)
                .SetEase(Ease.InOutBack))
            .Append(gameLogoImage.rectTransform.DOShakeRotation(0.2f, new Vector3(0, 0, 5), 10, 90))
            .AppendInterval(1.5f);
        explosionSequence.SetLoops(-1);
    }
}
