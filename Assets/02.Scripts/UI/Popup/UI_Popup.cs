using System;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    public EPopupName PopupName;
    public Action OnClose;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        PopupManager.Instance.PopupStack.Push(this);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        OnClose?.Invoke();
    }
}
