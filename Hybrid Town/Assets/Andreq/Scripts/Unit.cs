﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public List<Unit> LinkedUpUnit = new List<Unit>();
    public List<Unit> LinkedDownUnit = new List<Unit>();
    public List<Block> Blocks;

    public Guid Id;

    public int MaxHealth = 1;
    public bool IsDead { get; private set; } = false;

    public bool Chassis = false;

    private int _health;

    private Rigidbody2D _myRigidbody2D;
    private bool isFalling = false;

    public GameObject Explosion;


    private void Awake()
    {
        Id = Guid.NewGuid();
    }
    void Start()
    {
        Explosion = Resources.Load("Explosion") as GameObject;

        _health = MaxHealth;

        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _myRigidbody2D.isKinematic = true;

        GetLinkedLists();

        if (LinkedDownUnit.Count == 0 && !Chassis)
        {
            Died();
        }
    }

    public void GetDamage(int value)
    {
        if (value == 0)
            return;

        _health -= value;
        if (_health <= 0)
        {
            IsDead = true;
            Died();
        }
    }

    // Todo: если health > 0 => упасть и разрушиться
    private void Died()
    {
        DeleteFromLists();
        if (_health > 0)
        {
            // change this
            _health = 0;
            Remove();
            //gameObject.SetActive(false);
            SetFall();

            //Destroy(gameObject);
            //StartCoroutine(DestroyMy(0f));
        }
        else
        {
            _health = 0;
            Remove();
            Explore();
            gameObject.SetActive(false);
            //Destroy(gameObject);
            //StartCoroutine(DestroyMy(0f));
        }

    }

    private void SetFall()
    {
        _myRigidbody2D.isKinematic = false;
        //isFalling = true;
        StartCoroutine(DestroyMy(0f));
    }

    private void Remove()
    {
        LinkedDownUnit.ForEach(itm => itm.LinkedUpUnit.Remove(this));
    }

    IEnumerator DestroyMy(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFalling = true;
        // gameObject.SetActive(false);
        // Destroy(gameObject);
    }


    // мб тут анимацию и т.д. делать
    private void OnDestroy()
    {

    }

    public void GetLinkedLists()
    {
        UnitEqualityComparer unitComparer = new UnitEqualityComparer();

        var linkedLists = Blocks
                 .Select(itm => itm.CalculateLinkedUnit())
                 .ToList(); // надо 

        try
        {
            linkedLists.ForEach(list => LinkedDownUnit.AddRange(list.Item1.Where(itm => !Equals(itm.Id, Id))));
            linkedLists.ForEach(list => LinkedUpUnit.AddRange(list.Item2.Where(itm => !Equals(itm.Id, Id))));

        }
        catch
        {
            Debug.LogError("Сука " + gameObject.name);
        }

 


        LinkedDownUnit = LinkedDownUnit.Distinct(unitComparer).ToList();
        LinkedUpUnit = LinkedUpUnit.Distinct(unitComparer).ToList();

    }

    public void DeleteUnitLinkedListAndCheck(Unit unit)
    {
        LinkedDownUnit.Remove(unit);
        Check();

    }

    public void Check()
    {
        //bool result = LinkedDownUnit.All(itm => itm.gameObject.active == false);
        if (LinkedDownUnit.Count == 0 && !Chassis)
        {
            Died();
        }
    }

    public void DeleteFromLists()
    {
        LinkedUpUnit.ForEach(itm => {
            itm.DeleteUnitLinkedListAndCheck(this);
            //itm.Check();
            });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            gameObject.SetActive(false);
            Explore();
        }
    }

    private void Explore()
    {
        var bulletObj = (Instantiate(Explosion, transform.position, Quaternion.identity) as GameObject).transform;

    }
}

class UnitEqualityComparer : IEqualityComparer<Unit>
{
    public bool Equals(Unit b1, Unit b2)
    {
        return Equals(b1.Id, b2.Id);
    }

    public int GetHashCode(Unit bx)
    {
        return bx.GetHashCode();
    }
}
