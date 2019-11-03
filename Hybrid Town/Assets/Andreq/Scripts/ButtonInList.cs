using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInList : MonoBehaviour
{
    public static ButtonInList SelectedButton;
    public Panel Recipient;

    public ProductInList product;

    public Button button;
    private Image image;

    private bool isDisable = false;


    private void Start()
    {
        // EnableButtonUI();
    }
    public void OnClick()
    {
        if (isDisable)
            return;

        button = GetComponent<Button>();
        image = GetComponent<Image>();

        if (!isDisable && Recipient.GetMessage(gameObject))
        {
            if(SelectedButton != null)
                SelectedButton.UnSelect();
            SelectedButton = this;

            Select();
        }
    }

    public void Disable()
    {
        isDisable = true;
        DisbaleButtonUI();
    }

    // for UI
    private void EnableButtonUI() // == Unselect()
    {
        isDisable = false;
        UnSelect();
    }

    // for UI
    private void DisbaleButtonUI()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        image.color = Color.red;
    }

    // for UI
    private void Select()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        image.color = Color.green;
    }

    // for UI
    private void UnSelect()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        image.color = Color.white;
    }
}
