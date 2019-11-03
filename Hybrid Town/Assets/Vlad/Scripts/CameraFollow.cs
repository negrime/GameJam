using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    public bool isReady = false;

    public float smoothX;
    public float smoothY;
    private Vector2 velocity;

    // Update is called once per frame
    private void Start()
    {
    }
    
    public void SetReady()
    {
        isReady = true;
    }
    void FixedUpdate()
    {
        if (isReady)
        {
            GetComponent<Animator>().enabled = false;
            float x = Mathf.SmoothDamp(transform.position.x, _target.position.x, ref velocity.x, smoothX);
            float y = Mathf.SmoothDamp(transform.position.y, _target.position.y + .5f, ref velocity.y, smoothY);
            
            transform.position = new Vector3(x, y, transform.position.z);
            // transform.position = new Vector3(_target.position.x, _target.position.y + .5f, transform.position.z);
            //      Vector3.Lerp(new Vector2(transform.position.x, transform.position.y), 
            // new Vector3(_target.position.x, _target.position.y + .5f , transform.position.z), 100);
        }

        //new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
