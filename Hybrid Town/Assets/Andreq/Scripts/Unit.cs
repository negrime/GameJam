using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int MaxHealth { get; set; } = 1;
    public bool IsDead { get; private set; } = false;

    private int _health;

    private Rigidbody2D _myRigidbody2D;

    void Start()
    {
        _health = MaxHealth;

        _myRigidbody2D = GetComponent<Rigidbody2D>();
        _myRigidbody2D.isKinematic = true;
    }

    public void GetDamage(int value)
    {
        _health -= value;
        if(_health <= 0)
        {
            IsDead = true;
            Dead();
        }
    }

    // Todo: create this
    private void Dead()
    {
        _health = 0;

        StartCoroutine(DestroyMy(0f));
    }

    IEnumerator DestroyMy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // мб тут анимацию и т.д. делать
    private void OnDestroy()
    {

    }
}
