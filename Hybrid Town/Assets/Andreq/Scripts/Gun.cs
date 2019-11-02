using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Clickable
{
    [SerializeField]
    private float maxStretch = 3.0f;
    [SerializeField]
    private Transform MovementPart;
    [SerializeField]
    private Transform BulletPoint;
    [SerializeField]
    private float SliderRadius = 0f;
    [SerializeField]
    private float Force = 400f;

    [SerializeField]
    private LineRenderer catapultLine;

    [SerializeField]
    private GameObject Slider;

    [SerializeField]
    private GameObject[] PrefabBullet = null;

    private Vector2 startSliderPosition;
    private Bullet BulletComponent;

    private Ray _rayToMouse;
    private Ray _leftCatapultToProjectile;
    private float _maxStretchSqr;
    private bool _clickedOn;

    private bool isReady = false;
    private int typeBullet = 0;

    void Start()
    {
        _rayToMouse = new Ray(MovementPart.position, Vector3.zero);
        _leftCatapultToProjectile = new Ray(catapultLine.transform.position, Vector3.zero);
        _maxStretchSqr = maxStretch * maxStretch;
        LineRendererSetup();
        LineRendererUpdate();

        startSliderPosition = Slider.transform.position;
        ShowSlider(false);
    }

    void Update()
    {
        if (isReady && _clickedOn)
        {
            Dragging();
            LineRendererUpdate();
        }
    }

    void Shoot()
    {
       if(CreateBullet())
        {
            Vector2 direction = MovementPart.position - Slider.transform.position;
            var distance = Vector2.Distance(MovementPart.position, Slider.transform.position) / maxStretch;

            BulletComponent.Shoot(direction * Force * distance);
            catapultLine.SetPosition(1, catapultLine.GetPosition(0));

            Slider.transform.position = startSliderPosition;
            _clickedOn = false;
            BulletComponent = null;
        }

    }

    void LineRendererSetup()
    {
        catapultLine.SetPosition(0, catapultLine.transform.position);
        catapultLine.sortingLayerName = "Foreground";
        catapultLine.sortingOrder = 1;
    }

    public void OnMouseDownSlider()
    {
        if(isReady)
        {
            _clickedOn = true;
        }
    }

    public void OnMouseUpSlider()
    {
        if (isReady)
        {
            _clickedOn = false;
            Shoot();
        }
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - MovementPart.position;
        if (catapultToMouse.sqrMagnitude > _maxStretchSqr)
        {
            _rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = _rayToMouse.GetPoint(maxStretch);
        }
        mouseWorldPoint.z = 0f;
        Slider.transform.position = mouseWorldPoint;

        var turn = Quaternion.Lerp(MovementPart.rotation, Quaternion.LookRotation(Vector3.forward, Slider.transform.position - MovementPart.position), Time.deltaTime * 1.8f);

        MovementPart.Rotate(turn.eulerAngles);

        MovementPart.GetChild(0).LookAt(Slider.transform);
    }

    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = Slider.transform.position - catapultLine.transform.position;
        _leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = _leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + SliderRadius);
        catapultLine.SetPosition(1, holdPoint);
    }

    private bool CreateBullet()
    {
        if(PrefabBullet != null && PrefabBullet.Length > typeBullet && typeBullet >= 0)
        {
            var bulletObj = (Instantiate(PrefabBullet[typeBullet], BulletPoint.position, Quaternion.identity) as GameObject).transform;
            BulletComponent = bulletObj.GetComponent<Bullet>();
            Debug.Log("ss");
            return true;
        }
        return false;
    }

    protected override void ActionClick()
    {
        ShowSlider(true);
        Debug.Log("Click  " + gameObject.name);
    }

    protected override void ActionUnClicked()
    {
        ShowSlider(false);
        Debug.Log("UnClick  " + gameObject.name);
    }

    void ShowSlider(bool value)
    {
        isReady = value;
        Slider.SetActive(value);
        catapultLine.gameObject.SetActive(value);
    }

}