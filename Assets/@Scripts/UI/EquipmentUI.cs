using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentUI : BaseUI
{
    public class EquipDetailInfo
    {
        public string ItemName;
        public int Damage;
        public int Speed;
        public int Price;
        public Action OnPurchaseAction;
    }
    
    public Button closeButton;

    public Image categoryImage;

    public Button[] itemButtons;

    public TMP_Text currentMinerText;

    #region EquipPopupDetail

    [Header("DetailPopup")]
    public GameObject equipPopupDetail;
    public Button detailPopupPurchaseButton;
    public Button detailPopupEquipButton;
    public Button[] detailPopupCloseButtons;
    public Image itemImage;
    public Image payTypeImage;
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
    }

    public override void OpenUI()
    {
        base.OpenUI();
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
            OnPurchaseAction = () => HandleMiningToolUpgrade(0, 1, -100)
        });

        buttonToEquipDetailInfo.TryAdd(itemButtons[1], new EquipDetailInfo
        {
            ItemName = "BOMB",
            Damage = 20,
            Speed = 8,
            Price = 500,
            OnPurchaseAction = () => HandleMiningToolUpgrade(1, 1, -500)
        });
        
        buttonToEquipDetailInfo.TryAdd(itemButtons[2], new EquipDetailInfo
        {
            ItemName = "Turret",
            Damage = 20,
            Speed = 8,
            Price = 500,
            OnPurchaseAction = () => HandleMiningToolUpgrade(2, 1, -500)
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