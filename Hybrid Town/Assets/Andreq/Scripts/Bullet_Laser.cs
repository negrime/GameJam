using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Laser : Bullet
{
    public Transform pointScale;
    public float Maximum = 25;

    public float delay;

    protected override void GiveDamage(Unit unit)
    {
        unit.GetDamage(Damage);
    }

    public override void Shoot(Vector3 power)
    {
        transform.rotation = Quaternion.Euler(power.x, power.y, power.z + 90);
        transform.localScale = new Vector2(Mathf.Lerp(transform.localScale.x, Maximum, Time.time), transform.localScale.y);
        StartCoroutine(DestroyMy(delay));
    }

    // ToDo: сделать, чтобы лазер не заходил за непорожаемые объекты.
    private void OnTriggerEnter2D(Collider2D collision)
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
}
