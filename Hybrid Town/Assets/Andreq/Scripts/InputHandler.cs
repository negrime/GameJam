using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    /*
    public GameObject hoveredGO, lastHoveredGO;
    public enum HoverState { HOVER, NONE };
    public HoverState hover_state = HoverState.NONE;
    [HideInInspector] public Collider2D currentButton = null;

    void Update()
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D[] currentButtons = Physics2D.OverlapPointAll(touchPos);
        if (currentButtons.Length > 0)
        {

            float closestPosition = Mathf.Infinity;
            foreach (Collider2D col in currentButtons)
            {
                if (col.transform.position.z < closestPosition)
                {
                    closestPosition = col.transform.position.z;
                    currentButton = col;
                }
            }
            if (hover_state == HoverState.NONE)
            {
                currentButton.SendMessage("OnMouseButtonEnter", SendMessageOptions.DontRequireReceiver);
                hoveredGO = currentButton.gameObject;
            }
            else if (currentButton != lastHoveredGO)
            {
                lastHoveredGO.SendMessage("OnMouseButtonExit", SendMessageOptions.DontRequireReceiver);
                hoveredGO = currentButton.gameObject;
            }
            hover_state = HoverState.HOVER;
        }
        else
        {
            if (hover_state == HoverState.HOVER)
            {
                hoveredGO.SendMessage("OnMouseButtonExit", SendMessageOptions.DontRequireReceiver);
            }
            hoveredGO = null;
            hover_state = HoverState.NONE;
        }

        if (hover_state == HoverState.HOVER)
        {
            currentButton.SendMessage("OnMouseButtonOver", SendMessageOptions.DontRequireReceiver); //Mouse is hovering
            if (Input.GetMouseButtonDown(0))
            {
                currentButton.SendMessage("OnMouseButtonDown", SendMessageOptions.DontRequireReceiver); //Mouse down
            }
            if (Input.GetMouseButtonUp(0))
            {
                currentButton.SendMessage("OnMouseButtonUpAsButton", SendMessageOptions.DontRequireReceiver); //Mouse up as button
            }

        }
        lastHoveredGO = hoveredGO;
    }
    */
}
