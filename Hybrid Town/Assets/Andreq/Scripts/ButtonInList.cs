using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInList : MonoBehaviour
{
    public Panel Recipient;

    public void OnClick()
    {
        Recipient.GetMessage(gameObject);
    }
}
