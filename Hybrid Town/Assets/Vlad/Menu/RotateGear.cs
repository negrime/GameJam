using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGear : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0,0, speed * Time.deltaTime);
    }
}
