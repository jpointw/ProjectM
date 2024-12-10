using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TMP_Text goldText;
    public TMP_Text greenGemText;
    public TMP_Text redGemText;
    
    public IUIView[] UIViews;
    public Button[] bottomButtons;
    
    
    void Start()
    {
        GameSystems.Data.OnGoldUpdated += OnHandleGoldChanged;
        GameSystems.Data.OnGreenGemUpdated += OnHandleGreenGemChanged;
        GameSystems.Data.OnRedGemUpdated += OnHandleRedGemChanged;

        for (int i = 0; i < bottomButtons.Length; i++)
        {
            var index = i;
            bottomButtons[i].onClick.AddListener(() =>
            {
                if (UIViews[index].IsOpen)
                {
                    UIViews[index].Close();
                }
                else
                {
                    for (var index1 = 0; index1 < UIViews.Length; index1++)
                    {
                        var view = UIViews[index1];
                        if (view.IsOpen)
                        {
                            view.Close();
                        }
                    }

                    UIViews[index].Open();
                }
            });
        }
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
}

public interface IUIView
{
    public bool IsOpen { get; set; }
    public void Open();
    public void Close();
}

