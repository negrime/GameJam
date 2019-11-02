using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(new Vector2(transform.position.x, transform.position.y), new Vector3(_target.position.x, _target.position.y, transform.position.z), 5);
            //new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
