using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderGun : MonoBehaviour
{
    [SerializeField]
    private Gun Gun;
    private bool? type;

    private void OnMouseButtonDown()
    {
        down = true;
        Gun.OnMouseDownSlider();
    }

    private void OnMouseButtonUpAsButton()
    {
        down = false;
        Gun.OnMouseUpSlider();
    }

    RaycastHit hit;
    Ray MyRay;
    bool down = false;

    void Update()
    { 
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(down)
            OnMouseButtonUpAsButton();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MyRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(MyRay.origin, MyRay.direction, 10000,LayerMask.GetMask("Slider"));

            if (hit.collider != null)
            {
                if(hit.collider.gameObject == gameObject)
                {
                    OnMouseButtonDown();
                }
            }
        }
    }

}
