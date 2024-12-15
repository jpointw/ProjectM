using System;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public Canvas canvas;
    public MainUI mainUI;
    
    public bool IsOpen { get; set; }

    private void Awake()
    {
        canvas = gameObject.GetComponent<Canvas>();
        mainUI = GetComponentInParent<MainUI>();
    }

    public virtual void OpenUI()
    {
        IsOpen = true;
        canvas.enabled = true;
    }

    public virtual void CloseUI()
    {
        IsOpen = false;
        canvas.enabled = false;
    }
}
