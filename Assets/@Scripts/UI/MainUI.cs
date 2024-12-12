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
    
    public BaseUI[] BaseUis;
    public Button[] bottomButtons;
    
    
    void Start()
    {
        GameSystems.Data.OnGoldUpdated += OnHandleGoldChanged;
        GameSystems.Data.OnGreenGemUpdated += OnHandleGreenGemChanged;
        GameSystems.Data.OnRedGemUpdated += OnHandleRedGemChanged;
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

