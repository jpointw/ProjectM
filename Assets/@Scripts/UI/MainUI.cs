using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TMP_Text goldText;
    public TMP_Text greenGemText;
    public TMP_Text redGemText;

    public BaseUI[] BaseUis;
    public Button[] bottomButtons;

    [Header("Slider")]
    public Canvas sliderCanvas;
    public Slider[] itemSliders;
    public bool[] itemChecks = new []{false, false, false};
    
    private int activeIndex = -1;

    void Start()
    {
       
        for (int i = 0; i < bottomButtons.Length; i++)
        {
            int index = i;
            bottomButtons[i].onClick.AddListener(() => OnBottomButtonClick(index));
        }

        goldText.text = GameSystems.Data.SaveData.gold.ToString();
        greenGemText.text = GameSystems.Data.SaveData.greenGem.ToString();
        redGemText.text = GameSystems.Data.SaveData.redGem.ToString();


        
        GameSystems.Data.OnGoldUpdated += OnHandleGoldChanged;
        GameSystems.Data.OnGreenGemUpdated += OnHandleGreenGemChanged;
        GameSystems.Data.OnRedGemUpdated += OnHandleRedGemChanged;

        foreach (var baseUi in BaseUis)
        {
            baseUi.CloseUI();
        }
        
        SliderCanvasEnable(false);
    }

    public void OnHandleGoldChanged(int goldValue)
    {
        goldText.text = goldValue.ToString();
    }

    public void OnHandleGreenGemChanged(int greenGemValue)
    {
        greenGemText.text = greenGemValue.ToString();
    }

    public void OnHandleRedGemChanged(int redGemValue)
    {
        redGemText.text = redGemValue.ToString();
    }

    public void OnBottomButtonClick(int index)
    {
        if (activeIndex == index)
        {
            SliderCanvasEnable(false);
            BaseUis[index].CloseUI();
            activeIndex = -1;
        }
        else
        {
            for (int i = 0; i < BaseUis.Length; i++)
            {
                if (i == index)
                {
                    BaseUis[i].OpenUI();
                }
                else
                {
                    BaseUis[i].CloseUI();
                }
            }
            activeIndex = index;
        }
    }

    public void SliderCanvasEnable(bool toggle)
    {
        sliderCanvas.enabled = toggle;
    }

    public void ActiveItemSLider(int index)
    {
        itemChecks[index] = true;
        itemSliders[index].value = 1f;
        itemSliders[index].gameObject.SetActive(true);
        sliderCanvas.enabled = true;
        switch (index)
        {
            case 0:
                GameSystems.MinerSystem.HandleStatusChanged(MinerExtensions.GetDamage() * 2,MinerExtensions.GetSpeed());
                break;
            case 1:
                GameSystems.MinerSystem.HandleStatusChanged(MinerExtensions.GetDamage(),MinerExtensions.GetSpeed() * 2);
                break;
        }
        DecreaseSlider(itemSliders[index], index).Forget();
    }

    private async UniTaskVoid DecreaseSlider(Slider slider, int index)
    {
        float duration = 60f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        GameSystems.MinerSystem.HandleStatusChanged(MinerExtensions.GetDamage(),MinerExtensions.GetSpeed());
        slider.value = 0f;
        itemChecks[index] = false;
        slider.gameObject.SetActive(false);
    }
}