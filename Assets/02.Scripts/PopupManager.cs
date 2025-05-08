using UnityEngine;
using System.Collections.Generic;
using System;

public class PopupManager : Singleton<PopupManager>
{
    public List<UI_Popup> UI_Popups = new List<UI_Popup>();
    public Stack<UI_Popup> PopupStack = new Stack<UI_Popup>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopupStack.Count > 0)
            {
                Close();
            }
            else
            {
                GameManager.Instance.Pause();
            }
        }
    }

    public void Open(EPopupName popupName)
    {
        foreach (UI_Popup popup in UI_Popups)
        {
            if (popup.PopupName == popupName)
            {
                popup.Open();
                return;
            }
        }
        Debug.LogWarning($"Popup with name {popupName} not found.");
    }

    public void Open(EPopupName popupName, Action closeCallBack)
    {
        foreach (UI_Popup popup in UI_Popups)
        {
            if (popup.PopupName == popupName)
            {
                popup.OnClose += closeCallBack;
                popup.Open();
                return;
            }
        }
        Debug.LogWarning($"Popup with name {popupName} not found.");
    }

    public void Close()
    {
        if (PopupStack.Count > 0)
        {
            UI_Popup popup = PopupStack.Pop();
            popup.Close();
        }
    }
}
