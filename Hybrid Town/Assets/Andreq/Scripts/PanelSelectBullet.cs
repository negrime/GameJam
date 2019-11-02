using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectBullet :  Panel
{
    [SerializeField]
    private GameObject TemplateButton;
    [SerializeField]
    private GameObject Content;

    private List<Bullet> Bullets;
    private GameObject OpenedObject;
    private List<Button> Buttons = new List<Button>();

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open(GameObject obj, List<Bullet> list)
    {
        OpenedObject = obj;
        Bullets = list;
        Fill();
        gameObject.SetActive(true);
    }

    public void Close(GameObject obj)
    {
        if (OpenedObject.Equals(obj))
        {
            Bullets = null;
            gameObject.SetActive(false);
            Clear();
        }
    }

    private void Fill()
    {
        Clear();
        foreach (var bullet in Bullets)
        {
            var button = Instantiate(TemplateButton, Content.transform) as GameObject;

            var product = bullet.GetProduct();

            button.GetComponent<Image>().sprite = product.Sprite;
            button.GetComponentInChildren<Text>().text = product.ToString();
            button.GetComponent<ButtonInList>().Recipient = this;
            Buttons.Add(button.GetComponent<Button>());
        }
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

    public void ClickOnItem(Button button)
    {
        OpenedObject?.GetComponent<Gun>()?.SelectType(Buttons.IndexOf(button));
    }

    public override void GetMessage(GameObject sender)
    {
        var button = sender.GetComponent<Button>();
        if (button != null)
            ClickOnItem(button);
    }
}
