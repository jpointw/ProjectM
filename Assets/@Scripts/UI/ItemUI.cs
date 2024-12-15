using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : BaseUI
{
    public Button[] itemButtons;
    
    public Button[] closeButtons;
    
    public TMP_Text[] itemAmountTexts; 
    


    public Action<int> OnMiningSpeedItemUsed;
    public Action<int> OnMinerSpeedItemUsed;
    public Action<int> OnKillEnemiesItemUsed;


    private void Start()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].interactable = GameSystems.Data.SaveData.itemAmount[i] > 0;
        }
        
        itemButtons[0].onClick.AddListener(OnClickedItem1Button);
        itemButtons[1].onClick.AddListener(OnClickedItem2Button);
        itemButtons[2].onClick.AddListener(OnClickedItem3Button);

        foreach (var closeButton in closeButtons)
        {
            closeButton.onClick.AddListener(CloseUI);
        }
    }

    public override void OpenUI()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].interactable = GameSystems.Data.SaveData.itemAmount[i] > 0;
            itemAmountTexts[i].text = $"x {GameSystems.Data.SaveData.itemAmount[i]}";
        }
        for (int i = 0; i < mainUI.itemChecks.Length; i++)
        {
             itemButtons[i].interactable = !mainUI.itemChecks[i] && GameSystems.Data.SaveData.itemAmount[i] > 0;
            itemAmountTexts[i].text = $"x {GameSystems.Data.SaveData.itemAmount[i]}";
        }
        base.OpenUI();
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }


    public void OnClickedItem1Button()
    {
        var itemAmout = GameSystems.Data.UpdateItemAmout(0, -1);
        if (itemAmout > 0)
        {
            itemAmountTexts[0].text = itemAmout.ToString();
        }
        else
        {
            itemAmountTexts[0].text = $"x {itemAmout}";
            itemButtons[0].interactable = false;
        }

        mainUI.ActiveItemSLider(0);
    }
    public void OnClickedItem2Button()
    {
        var itemAmout = GameSystems.Data.UpdateItemAmout(1, -1);
        if (itemAmout > 0)
        {
            itemAmountTexts[1].text = itemAmout.ToString();
        }
        else
        {
            itemAmountTexts[1].text = $"x {itemAmout}";
            itemButtons[1].interactable = false;
        }
        
        mainUI.ActiveItemSLider(1);

    }
    
    public void OnClickedItem3Button()
    {
        var itemAmout = GameSystems.Data.UpdateItemAmout(2, -1);
        if (itemAmout > 0)
        {
            itemAmountTexts[2].text = itemAmout.ToString();
        }
        else
        {
            itemAmountTexts[2].text = $"x {itemAmout}";
            itemButtons[2].interactable = false;
        }
        GameSystems.EnemySystem.KillAllEnemies();
    }
}
