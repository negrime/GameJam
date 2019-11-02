using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFight : MonoBehaviour
{
    [SerializeField]
    private PanelSelectBullet PanelSelectBullet;

    public static PanelSelectBullet SelectBullet;

    private void Awake()
    {
        SelectBullet = PanelSelectBullet;
    }
}
