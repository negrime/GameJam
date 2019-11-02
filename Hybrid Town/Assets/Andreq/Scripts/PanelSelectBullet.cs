using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelectBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject TemplateButton;
    [SerializeField]
    private GameObject Content;

    private List<Bullet> Bullets;
    private Guid OpenedObjectId;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Open(Guid Id, List<Bullet> list)
    {
        OpenedObjectId = Id;
        Bullets = list;
        Fill();
        gameObject.SetActive(true);
    }

    public void Close(Guid Id)
    {
        if (OpenedObjectId.Equals(Id))
        {
            Bullets = null;
            gameObject.SetActive(false);
            Clear();
        }
    }

    private void Fill()
    {
        foreach(var bullet in Bullets)
        {
            var button = Instantiate(TemplateButton, Content.transform) as GameObject;

            var product = bullet.GetProduct();

            button.GetComponent<Image>().sprite = product.Sprite;
            button.GetComponentInChildren<Text>().text = product.ToString();
        }
    }

    private void Clear()
    {
        while(Content.transform.childCount != 0)
        {
            Destroy(Content.transform.GetChild(0).gameObject);
        }
    }
}
