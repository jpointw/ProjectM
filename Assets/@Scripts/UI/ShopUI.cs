using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : BaseUI
{
    public class ShopDetailInfo
    {
        public string ItemName;
        public Sprite ItemSprite;
        public Sprite PayTypeSprite;
        public int Price;
        public Action OnPurchaseAction;
    }

    [Header("Purchase Buttons")]
    public Button[] goldPurchaseButtons;
    public Button[] itemPurchaseButtons;
    
    public Button closeButton;

    [Header("Popup Settings")]
    public GameObject shopDetailPopup;
    public Button detailPopupPurchaseButton;
    public Button[] detailPopupCloseButtons;
    public Image itemImage;
    public Image payTypeImage;
    public TMP_Text shopDetailPopupText;
    public TMP_Text shopDetailPriceText;

    [Header("Sprites")]
    public Sprite[] goldSprites;
    public Sprite[] itemSprites;
    public Sprite[] payTypeSprites;

    private Dictionary<Button, ShopDetailInfo> buttonToShopDetailInfo = new Dictionary<Button, ShopDetailInfo>();

    private void Start()
    {
        InitializeShopDetails();

        foreach (var kvp in buttonToShopDetailInfo)
        {
            kvp.Key.onClick.AddListener(() => OpenShopDetailPopup(kvp.Value));
        }

        foreach (var detailCloseButton in detailPopupCloseButtons)
        {
            detailCloseButton.onClick.AddListener(() => shopDetailPopup.SetActive(false));
        }
        closeButton.onClick.AddListener(CloseUI);
    }

    private void InitializeShopDetails()
    {
        TryAddShopDetail(goldPurchaseButtons[0], new ShopDetailInfo
        {
            ItemName = "10000 Gold",
            ItemSprite = goldSprites[0],
            PayTypeSprite = payTypeSprites[0],
            Price = 10,
            OnPurchaseAction = () => HandleGoldPurchase(10000, -10)
        });

        TryAddShopDetail(goldPurchaseButtons[1], new ShopDetailInfo
        {
            ItemName = "Green Gem x10",
            ItemSprite = goldSprites[1],
            PayTypeSprite = payTypeSprites[1],
            Price = 1,
            OnPurchaseAction = () => HandleGemPurchase(-1, 10)
        });

        TryAddShopDetail(goldPurchaseButtons[2], new ShopDetailInfo
        {
            ItemName = "Red Gem Purchase",
            ItemSprite = goldSprites[2],
            PayTypeSprite = payTypeSprites[2],
            Price = 0,
            OnPurchaseAction = HandleRedGemPurchase
        });

        TryAddShopDetail(itemPurchaseButtons[0], new ShopDetailInfo
        {
            ItemName = "Mining Speed Item",
            ItemSprite = itemSprites[0],
            PayTypeSprite = payTypeSprites[0],
            Price = 10000,
            OnPurchaseAction = () => HandleItemPurchase(0, 1, -10000)
        });

        TryAddShopDetail(itemPurchaseButtons[1], new ShopDetailInfo
        {
            ItemName = "Miner Speed Item",
            ItemSprite = itemSprites[1],
            PayTypeSprite = payTypeSprites[0],
            Price = 10000,
            OnPurchaseAction = () => HandleItemPurchase(1, 1, -10000)
        });

        TryAddShopDetail(itemPurchaseButtons[2], new ShopDetailInfo
        {
            ItemName = "Kill Enemy Item",
            ItemSprite = itemSprites[2],
            PayTypeSprite = payTypeSprites[0],
            Price = 10000,
            OnPurchaseAction = () => HandleItemPurchase(2, 1, -10000)
        });
    }

    private void TryAddShopDetail(Button button, ShopDetailInfo shopDetailInfo)
    {
        if (!buttonToShopDetailInfo.TryAdd(button, shopDetailInfo))
        {
            Debug.LogWarning($"Button {button.name} is already mapped to a ShopDetailInfo.");
        }
    }

    private void OpenShopDetailPopup(ShopDetailInfo shopDetailInfo)
    {
        shopDetailPopupText.text = shopDetailInfo.ItemName;
        itemImage.sprite = shopDetailInfo.ItemSprite;
        payTypeImage.sprite = shopDetailInfo.PayTypeSprite;
        shopDetailPriceText.text = shopDetailInfo.Price.ToString();

        detailPopupPurchaseButton.onClick.RemoveAllListeners();
        detailPopupPurchaseButton.onClick.AddListener(() =>
        {
            shopDetailInfo.OnPurchaseAction?.Invoke();
            shopDetailPopup.SetActive(false);
        });

        shopDetailPopup.SetActive(true);
    }

    private void HandleGoldPurchase(int goldAmount, int greenGemChange)
    {
        GameSystems.Data.UpdateGold(goldAmount);
        GameSystems.Data.UpdateGreenGem(greenGemChange);
    }

    private void HandleGemPurchase(int redGemChange, int greenGemChange)
    {
        GameSystems.Data.UpdateRedGem(redGemChange);
        GameSystems.Data.UpdateGreenGem(greenGemChange);
    }

    private void HandleRedGemPurchase()
    {
        //todo IAP 연결
    }

    private void HandleItemPurchase(int itemIndex, int amount, int goldCost)
    {
        GameSystems.Data.UpdateItemAmout(itemIndex, amount);
        GameSystems.Data.UpdateGold(goldCost);
    }


    public override void OpenUI()
    {
        base.OpenUI();
        shopDetailPopup.SetActive(false);
    }

    public override void CloseUI()
    {
        base.CloseUI();
        shopDetailPopup.SetActive(false);
    }
}
