﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<ProductInList> Products = null;

    private Vector2 startSliderPosition;
    private Quaternion startMovementPartRotation;
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
        startMovementPartRotation = MovementPart.rotation;
        ShowSlider(false);
    }

    void Update()
    {
        if (isReady && _clickedOn)
        {
            Dragging();
            LineRendererUpdate();
        }
        UpdateClickable();
    }

    protected virtual void Shoot()
    {
        if (CreateBullet())
        {
            Products[typeBullet].ReduceCount();

            Vector2 direction = MovementPart.position - Slider.transform.position;
            var distance = Vector2.Distance(MovementPart.position, Slider.transform.position) / maxStretch;

            BulletComponent.Shoot(direction * Force * distance);
            catapultLine.SetPosition(1, catapultLine.GetPosition(0));

            Slider.transform.position = startSliderPosition;
            _clickedOn = false;
            BulletComponent = null;
            StartCoroutine(RotateToStarWithDelay(0f));
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
        if (isReady)
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

        MovementPart.LookAt(Slider.transform, Vector3.forward);
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
        if (Products?.Capacity > typeBullet && typeBullet >= 0)
        {
            var bulletObj = (Instantiate(Products[typeBullet].prefab, BulletPoint.position, Quaternion.identity) as GameObject).transform;
            BulletComponent = bulletObj.GetComponent<Bullet>();
            return true;
        }
        return false;
    }

    void ShowSlider(bool value)
    {
        isReady = value;
        Slider.SetActive(value);
        catapultLine.gameObject.SetActive(value);
    }

    IEnumerator RotateToStarWithDelay(float delay, float power = 1f)
    {
        yield return new WaitForSeconds(delay);
        RotateFromStart(power);
        if (MovementPart.rotation != startMovementPartRotation)
            StartCoroutine(RotateToStarWithDelay(0f, power * 1.2f));
    }

    void RotateFromStart(float power = 1)
    {
        MovementPart.rotation = Quaternion.Lerp(MovementPart.rotation, startMovementPartRotation, Time.deltaTime * power);

    }

    protected override void ActionClick()
    {
        ShowSlider(true);
        // Debug.Log("Click  " + gameObject.name);
        CanvasFight.SelectBullet.Open(gameObject, Products);
    }

    protected override void ActionUnClicked()
    {
        ShowSlider(false);
        CanvasFight.SelectBullet.Close(gameObject);
        // Debug.Log("UnClick  " + gameObject.name);
    }

    public void SelectType(int type = -1)
    {
        Debug.Log(type);
        typeBullet = type;
    }
}