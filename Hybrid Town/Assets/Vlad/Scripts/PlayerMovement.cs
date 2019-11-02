using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    public float size;
    public float speed;
    public float rotateSpeed;
    private float x, y;
    void Start()
    {
        size = transform.localScale.x;
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.localScale = new Vector3(size, size, 0);
        x = Input.GetAxis("Horizontal");
        y  = Input.GetAxis("Vertical");
      //  _movement = new Vector2(x, y);
        //_rb.velocity = new Vector3(0, 10, 0); 
        
        transform.Translate(new Vector2(0, speed)  * Time.deltaTime);
        

        transform.Rotate(Vector3.forward, -x * rotateSpeed * Time.deltaTime, Space.Self);
     //   transform.Translate(_movement * speed * Time.deltaTime);
        //_rb.velocity = _movement * speed;
         //+= _movement.x
        //transform.rotation = Quaternion.Euler(0, 0,  transform.rotation.z+ _movement.x);
       //Quaternion target = Quaternion.Euler(_movement.x, 0, _movement.x);

        // Dampen towards the target rotation
       /// transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * speed);
      //  transform.rotation = Quaternion.Euler(new Vector3(0,0,transform.rotation.z -  _movement.x));
       // _rb.SetRotation(_rb.rotation -  _movement.x);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wood"))
        {
            GameManager.gm.wood++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Iron"))
        {
            GameManager.gm.iron++;
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Stone"))
        {
            GameManager.gm.stone++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Есть контакт");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000);
        }
        
    }
}
