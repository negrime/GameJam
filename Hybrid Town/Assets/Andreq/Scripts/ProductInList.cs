using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ProductInList
{
    public GameObject prefab;
    public Sprite Sprite;
    public string Description;
    public int Damage;
    public int Count;

    private ButtonInList button;

    public override string ToString()
    {
        string str = "";
        str += "Урон: " + Damage + "\n";
        str += "Кол-во: " + Count;

        return str;
    }

    public void Reduce(int value = 1)
    {
        Count -= value;
        UpdateButton();
    }

    public void AddButton(ButtonInList _button)
    {
        button = _button;
        button.product = this;
    }

    private void UpdateButton()
    {
        button.gameObject.GetComponentInChildren<Text>().text = ToString();
        if (Count <= 0)
        {
            CanvasFight.SelectBullet.UpdateScreenData();
            button.Disable();
        }

    }

}
