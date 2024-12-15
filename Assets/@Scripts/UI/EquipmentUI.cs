using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class EquipmentUI : BaseUI
{
    public class EquipDetailInfo
    {
        public string ItemName;
        public int Damage;
        public int Speed;
        public int Price;
        public Sprite Icon;
        public Action OnPurchaseAction;
    }
    public TMP_Text levelText;
    public Slider[] statSliders;
    
    public Button closeButton;

    public Image categoryImage;

    public Button characterAddButton;
    public Button[] itemButtons;

    public TMP_Text currentMinerText;
    
    public Sprite[] itemIcons;
    
    #region EquipPopupDetail

    [Header("DetailPopup")]
    public GameObject equipPopupDetail;
    public Button detailPopupPurchaseButton;
    public Button detailPopupEquipButton;
    public Button[] detailPopupCloseButtons;
    public TMP_Text shopDetailPopupText;
    public TMP_Text shopDetailPriceText;
    public TMP_Text itemDamageText;
    public TMP_Text itemSpeedText;

    #endregion

    private Dictionary<Button, EquipDetailInfo> buttonToEquipDetailInfo = new Dictionary<Button, EquipDetailInfo>();

    private void Start()
    {
        InitializeEquipDetails();

        foreach (var kvp in buttonToEquipDetailInfo)
        {
            kvp.Key.onClick.AddListener(() => ShowEquipDetailPopup(kvp.Value));
        }

        foreach (var button in detailPopupCloseButtons)
        {
            button.onClick.AddListener(CloseEquipDetailPopup);
        }
        
        closeButton.onClick.AddListener(CloseUI);
        
        if (GameSystems.Data.SaveData.minerCount >= 10)
        {
            characterAddButton.enabled = false;
        }
    }

    public override void OpenUI()
    {
        base.OpenUI();
        currentMinerText.text = GameSystems.Data.SaveData.minerCount.ToString();
        levelText.text = GameSystems.Data.SaveData.level.ToString();
        statSliders[0].value = Mathf.Clamp01(MinerExtensions.GetDamage() / 2000);
        statSliders[1].value = Mathf.Clamp01(MinerExtensions.GetSpeed() / 100);
        equipPopupDetail.SetActive(false);
    }

    public override void CloseUI()
    {
        base.CloseUI();
        equipPopupDetail.SetActive(false);
    }

    private void InitializeEquipDetails()
    {
        buttonToEquipDetailInfo.TryAdd(itemButtons[0], new EquipDetailInfo
        {
            ItemName = "AXE",
            Damage = 10,
            Speed = 5,
            Price = 100,
            Icon = itemIcons[0],
            OnPurchaseAction = () => HandleMiningToolUpgrade(0, 1, -100)
        });

        buttonToEquipDetailInfo.TryAdd(itemButtons[1], new EquipDetailInfo
        {
            ItemName = "BOMB",
            Damage = 20,
            Speed = 8,
            Price = 500,
            Icon = itemIcons[1],
            OnPurchaseAction = () => HandleMiningToolUpgrade(1, 1, -500)
        });
        
        buttonToEquipDetailInfo.TryAdd(itemButtons[2], new EquipDetailInfo
        {
            ItemName = "Turret",
            Damage = 20,
            Speed = 8,
            Price = 500,
            Icon = itemIcons[2],
            OnPurchaseAction = () => HandleMiningToolUpgrade(2, 1, -500)
        });

        buttonToEquipDetailInfo.TryAdd(characterAddButton, new EquipDetailInfo
        {
            ItemName = "Character",
            Damage = 30,
            Speed = 30,
            Price = 10000,
            Icon = null,
            OnPurchaseAction = () =>
            {
                GameSystems.Data.SaveData.minerCount++;
                GameSystems.Data.OnGoldUpdated(-10000);
                GameSystems.MinerSystem.OnMinerAdded.Invoke();
                currentMinerText.text = GameSystems.Data.SaveData.minerCount.ToString();
            }
        });
    }

    public void ShowEquipDetailPopup(EquipDetailInfo equipDetailInfo)
    {
        shopDetailPopupText.text = equipDetailInfo.ItemName;
        itemDamageText.text = equipDetailInfo.Damage.ToString();
        itemSpeedText.text = equipDetailInfo.Speed.ToString();
        shopDetailPriceText.text = equipDetailInfo.Price.ToString();
        detailPopupEquipButton.onClick.RemoveAllListeners();
        detailPopupPurchaseButton.onClick.RemoveAllListeners();
        detailPopupPurchaseButton.onClick.AddListener(() =>
        {
            Debug.LogError("isClicked");
            equipDetailInfo.OnPurchaseAction?.Invoke();
            CloseEquipDetailPopup();
        });

        equipPopupDetail.SetActive(true);
    }

    public void CloseEquipDetailPopup()
    {
        equipPopupDetail.SetActive(false);
    }

    private void HandleMiningToolUpgrade(int itemIndex, int amount, int goldCost)
    {
        GameSystems.Data.UpdateMiningTool(itemIndex, amount);
        GameSystems.Data.UpdateGold(goldCost);
    }
}