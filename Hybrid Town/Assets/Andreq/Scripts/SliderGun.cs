using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderGun : MonoBehaviour
{
    [SerializeField]
    private Gun Gun;

    private void OnMouseDown()
    {
        Gun.OnMouseDownSlider();
    }

    private void OnMouseUp()
    {
        Gun.OnMouseUpSlider();
    }
}
