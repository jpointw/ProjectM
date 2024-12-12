using System;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public Canvas canvas;
    
    public bool IsOpen { get; set; }

    private void Awake()
    {
        canvas ??= gameObject.GetComponent<Canvas>();
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
