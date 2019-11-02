using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInList : MonoBehaviour
{
    public static ButtonInList SelectedButton;
    public Panel Recipient;

    private Button button;
    private Image image;

    private bool isDisable = false;


    public void OnClick()
    {
        if (!isDisable && Recipient.GetMessage(gameObject))
        {
            SelectedButton?.UnSelect();
            SelectedButton = this;

            Select();
        }
    }

    public void Disable(bool value)
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        isDisable = value;

        if (value)
        {
            EnableButtonUI();
        }
        else
        {
            DisbaleButtonUI();
        }
    }

    // for UI
    private void EnableButtonUI() // == Unselect()
    {
        UnSelect();
    }

    // for UI
    private void DisbaleButtonUI()
    {
        image.color = Color.red;
    }

    // for UI
    private void Select()
    {
        image.color = Color.yellow;
    }

    // for UI
    private void UnSelect()
    {
        image.color = Color.white;
    }
}
