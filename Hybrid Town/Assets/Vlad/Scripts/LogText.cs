using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogText : MonoBehaviour
{
    [SerializeField]
    private Vector2 _dir;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timeToDestroy;
    void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }

    void Update()
    {
        transform.Translate(_dir * _speed * Time.deltaTime);
    }
}
