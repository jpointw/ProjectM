using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : BaseUI
{
    public Image caegoryImage;
    
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


    private void Start()
    {
        itemButtons[0].onClick.AddListener();
        itemButtons[0].onClick.AddListener();
        itemButtons[0].onClick.AddListener();
    }

    public override void OpenUI()
    {
        base.OpenUI();
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }
}
