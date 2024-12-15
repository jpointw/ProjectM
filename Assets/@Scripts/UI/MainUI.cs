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
    
    private int activeIndex = -1;

    void Start()
    {
       
        for (int i = 0; i < bottomButtons.Length; i++)
        {
            int index = i;
            bottomButtons[i].onClick.AddListener(() => OnBottomButtonClick(index));
        }

        
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
        itemSliders[index].value = 1f;
        itemSliders[index].gameObject.SetActive(true);

        DecreaseSlider(itemSliders[index]).Forget();
    }

    private async UniTaskVoid DecreaseSlider(Slider slider)
    {
        float duration = 60f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        slider.value = 0f;
        slider.gameObject.SetActive(false);
    }
}