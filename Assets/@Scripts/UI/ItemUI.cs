using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : BaseUI
{
    public Button[] itemButtons;
    
    public TMP_Text item1AmountText; 
    public TMP_Text item2AmountText; 
    public TMP_Text item3AmountText; 


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

    }


    public void OnClickedItem1Button()
    {
        var itemAmout = GameSystems.Data.UpdateItemAmout(0, -1);
        if (itemAmout > 0)
        {
            item2AmountText.text = itemAmout.ToString();
        }
        else
        {
            item2AmountText.text = $"x {itemAmout}";
            itemButtons[1].interactable = false;
        }
        GameSystems.MinerSystem.OnStatusChanged.Invoke(0,0);
    }
    public void OnClickedItem2Button()
    {
        var itemAmout = GameSystems.Data.UpdateItemAmout(1, -1);
        if (itemAmout > 0)
        {
            item2AmountText.text = itemAmout.ToString();
        }
        else
        {
            item2AmountText.text = $"x {itemAmout}";
            itemButtons[1].interactable = false;

        }
        GameSystems.MinerSystem.OnStatusChanged.Invoke(0,0);
    }
    
    public void OnClickedItem3Button()
    {
        var itemAmout = GameSystems.Data.UpdateItemAmout(2, -1);
        if (itemAmout > 0)
        {
            item2AmountText.text = itemAmout.ToString();
        }
        else
        {
            item2AmountText.text = $"x {itemAmout}";
            itemButtons[1].interactable = false;
        }
        GameSystems.EnemySystem.KillAllEnemies();
    }
}
