using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    protected static Clickable SelectedObject;

    private void Click()
    {
        SelectedObject = this;
        ActionClick();
    }

    protected void UpdateClickable()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SelectedObject?.ActionUnClicked();
            SelectedObject = null;
        }
    }

    private void OnMouseDown()
    {
        if (!this.Equals(SelectedObject))
        {
            SelectedObject?.ActionUnClicked();
            Click();
        }

    }

    protected virtual void ActionClick()
    {

    }

    protected virtual void ActionUnClicked()
    {

    }
}
