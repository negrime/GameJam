using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ProductInList;

public class Bullet : MonoBehaviour
{
    public int Damage = 1;

    protected Rigidbody2D myRigidbody2d;

    private void Awake()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
    }

    public virtual void Shoot(Vector3 power)
    {

        myRigidbody2d.isKinematic = false;
        GetComponent<Collider2D>().isTrigger = false;
        myRigidbody2d.angularDrag = 5f;
        myRigidbody2d.AddForce(power);

        StartCoroutine(DestroyMy(5f));
    }

    protected virtual void GiveDamage(Unit unit)
    {
        unit.GetDamage(Damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();
        if (unit == null)
        {
            unit = collision.gameObject.GetComponentInParent<Unit>();
        }

        if (unit != null)
        {
            GiveDamage(unit);
            StartCoroutine(DestroyMy(0f));
            Damage = 0;
        }
    }

    protected IEnumerator DestroyMy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // мб тут анимацию и т.д. делать
    private void OnDestroy()
    {
        
    }

}
