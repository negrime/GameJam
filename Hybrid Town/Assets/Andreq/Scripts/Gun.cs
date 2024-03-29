﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : Clickable
{
    [SerializeField]
    protected float maxStretch = 0.5f;
    [SerializeField]
    protected Transform MovementPart;
    [SerializeField]
    protected Transform BulletPoint;
    [SerializeField]
    protected float SliderRadius = 0f;
    [SerializeField]
    protected float Force = 400f;

    [SerializeField]
    protected LineRenderer catapultLine;

    [SerializeField]
    protected GameObject Slider;

    [SerializeField]
    protected List<ProductInList> Products = null;

    protected Vector2 startSliderPosition;
    private Quaternion startMovementPartRotation;
    protected Bullet BulletComponent;

    protected Ray _rayToMouse;
    private Ray _leftCatapultToProjectile;
    protected float _maxStretchSqr;
    protected bool _clickedOn;

    private bool isReady = false;
    protected int typeBullet = 0;

    public bool QueueShoot = false;

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

        Products.ForEach(itm => itm.prefab.GetComponent<Bullet>().Damage = itm.Damage);
        catapultLine.useWorldSpace = true;
    }

    void Update()
    {
        if (isReady && _clickedOn && typeBullet != -1 && QueueShoot)
        {
            Dragging();
            LineRendererUpdate();
        }
        UpdateClickable();
    }

    protected virtual void Shoot()
    {
        if (CreateBullet() && QueueShoot)
        {
            Game.ChangeQueue();
            Products[typeBullet].Reduce();

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
        if (isReady && QueueShoot)
        {
            _clickedOn = true;
        }
    }

    public void OnMouseUpSlider()
    {
        if (isReady && QueueShoot)
        {
            _clickedOn = false;
            Shoot();
        }
    }

    protected virtual void Dragging()
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

    protected virtual bool CreateBullet()
    {
        if (Products?.Capacity > typeBullet && typeBullet >= 0)
        {
            OnTrigger();
            var bulletObj = (Instantiate(Products[typeBullet].prefab, BulletPoint.position, Quaternion.identity) as GameObject).transform;
            BulletComponent = bulletObj.GetComponent<Bullet>();
            return true;
        }
        return false;
    }

    void ShowSlider(bool value)
    {   if(value)
        {
            if(QueueShoot)
            {
                isReady = value;
                Slider.SetActive(value);
                catapultLine.gameObject.SetActive(value);
            }
        } else
        {
            isReady = value;
            Slider.SetActive(value);
            catapultLine.gameObject.SetActive(value);
        }

    }

    protected IEnumerator RotateToStarWithDelay(float delay, float power = 1f)
    {
        ShowSlider(false);
        yield return new WaitForSeconds(delay);
        RotateFromStart(power);
        if (Math.Abs(MovementPart.rotation.eulerAngles.z - startMovementPartRotation.eulerAngles.z) > 0.01f)
            StartCoroutine(RotateToStarWithDelay(0f, power * 1.2f));
        else
            ShowSlider(true);

    }

    void RotateFromStart(float power = 1)
    {
        MovementPart.rotation = Quaternion.Lerp(MovementPart.rotation, startMovementPartRotation, Time.deltaTime * power);

    }

    protected override void ActionClick()
    {
        ShowSlider(true);
        // Debug.Log("Click  " + gameObject.name);
        if(QueueShoot)
        CanvasFight.SelectBullet.Open(gameObject, Products);
    }


    public void CloseWindow()
    {
        ActionUnClicked();
    }

    protected override void ActionUnClicked()
    {
        ShowSlider(false);
        CanvasFight.SelectBullet.Close(gameObject);
        // Debug.Log("UnClick  " + gameObject.name);
    }

    public void SelectType(int type = -1)
    {
        typeBullet = type;
    }

    protected void OnTrigger()
    {
        GetComponents<Collider2D>().ToList().ForEach(itm => itm.isTrigger = true);
        StartCoroutine(OffTrigger(0.15f));
    }

    IEnumerator OffTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponents<Collider2D>().ToList().ForEach(itm => itm.isTrigger = false);
    }
}