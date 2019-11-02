using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Vector2 _movePoint;
    [SerializeField]
    private int _speed;
    void Start()
    {
        _movePoint = NewTarget();
    }

    void Update()
    {
        Vector3 vectorToTarget = _movePoint - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(-angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _speed);
        
        if (Vector2.Distance(transform.position, _movePoint) < 0.2f)
        {
            _movePoint = NewTarget();
        }
        transform.position =  Vector2.MoveTowards(transform.position, _movePoint, _speed * Time.deltaTime);
    }


    private Vector2 NewTarget()
    {
        return new Vector3(Random.Range(0, 10), Random.Range(0, 19));
    }
}
