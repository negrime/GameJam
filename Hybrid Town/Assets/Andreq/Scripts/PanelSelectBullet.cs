using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectBullet : Panel
{
    [SerializeField]
    private GameObject TemplateButton;
    [SerializeField]
    private GameObject Content;

    private List<ProductInList> Products;
    private GameObject OpenedObject;
    private List<Button> Buttons = new List<Button>();

    private bool open = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open(GameObject obj, List<ProductInList> list)
    {
        open = true;
        OpenedObject = obj;
        Products = list;
        Fill();
        gameObject.SetActive(true);
        Buttons.FirstOrDefault()?.GetComponent<ButtonInList>().OnClick();
    }

    public void Close(GameObject obj)
    {

        if (OpenedObject.Equals(obj))
        {
            open = false;

            Products = null;
            gameObject.SetActive(false);
            Clear();
        }
    }

    private void Fill()
    {
        Clear();

        Debug.Log(Products.Count);
        foreach (var product in Products)
        {
            Debug.Log(Products.Count);
            var button = Instantiate(TemplateButton, Content.transform) as GameObject;

            UpdateButtons(button, product);
            Buttons.Add(button.GetComponent<Button>());
        }
    }

    private void UpdateButtons(GameObject button, ProductInList product)
    {
        button.GetComponent<Image>().sprite = product.Sprite;
        button.GetComponentInChildren<Text>().text = product.ToString();
        button.GetComponent<ButtonInList>().Recipient = this;

        button.GetComponent<ButtonInList>().Disable(product.Count <= 0);
    }

    private void Clear()
    {
        Buttons.Clear();
        Content.transform.GetComponentsInChildren<Transform>()
            .ToList()
            .ForEach(itm =>
            {
                if (!itm.gameObject.Equals(Content))
                    Destroy(itm.gameObject);
            });
    }

    public bool ClickOnItem(Button button)
    {
        var value = Buttons.IndexOf(button);
        if (value != -1)
        {
            OpenedObject?.GetComponent<Gun>()?.SelectType(value);
            return true;
        }
        return false;

    }

    public override bool GetMessage(GameObject sender)
    {
        var button = sender.GetComponent<Button>();
        if (button != null)
        {
            return ClickOnItem(button);
        }
        return false;
    }

    public void UpdateDataOnScreen()
    {

            for (int i = 0; i < 0; i++)
            {
                UpdateButtons(Buttons[i].gameObject, Products[i]);
            }
        

    }
}
