using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    public float size;
    public float speed;
    public float rotateSpeed;
    private float x, y;

    public GameObject mouth;
    private bool isharppon = false;
    public GameObject harppon;

    private float time;
    [SerializeField] 
    private float harpponTime;

    private bool isNotificated = false;

    private Transform _canvasTransform;
    void Start()
    {
        _canvasTransform = FindObjectOfType<Canvas>().transform;
        time = 0;
        size = transform.localScale.x;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FalseHarppon()
    {
        isharppon = false;
    }

    private void Update()
    {
        if (time >= 0)
            time -= Time.deltaTime;
        else if (!isNotificated)
        {
            isNotificated = true;
            var go = Resources.Load("LogText") as GameObject;
            go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + 1),
                Quaternion.identity, _canvasTransform);
            //go.GetComponent<Text>().color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (time <= 0)
            {
                //isharppon
                harppon.SetActive(true);
                time = harpponTime;
                isNotificated = false;
            }
            else
            {
                var go = Resources.Load("LogText") as GameObject;
                go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + 1),
                    Quaternion.identity, _canvasTransform);
                go.GetComponent<Text>().text = "Гарпун перезаряжается";
            }
        }
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
        if (other.gameObject.CompareTag("Enemy") && !harppon.activeSelf)
        {
            mouth.SetActive(true);
            var go = Resources.Load("DestroyCity") as GameObject;
            go = Instantiate(go, new Vector2(other.transform.position.x, other.transform.position.y),
                Quaternion.identity);
                go = Resources.Load("DestroyCity") as GameObject;
            go = Instantiate(go, new Vector2(other.transform.position.x, other.transform.position.y),
                Quaternion.identity);
            
            
            GameManager.gm.wood += 25;
            GameManager.gm.stone += 15;
            GameManager.gm.iron += 5;
            Destroy(other.gameObject);
            
            go = Resources.Load("LogText") as GameObject;
            go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + 1.5f),
                Quaternion.identity, _canvasTransform);
            go.GetComponent<Text>().text = "+ 25 дерева";
            
             go = Resources.Load("LogText") as GameObject;
            go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + 1.25f),
                Quaternion.identity, _canvasTransform);
            go.GetComponent<Text>().text = "+ 15 камня";
            
             go = Resources.Load("LogText") as GameObject;
            go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + 1),
                Quaternion.identity, _canvasTransform);
            go.GetComponent<Text>().text = "+ 5 железа";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wood"))
        {
            GameManager.gm.wood++;
            Destroy(other.gameObject);
            var go = Resources.Load("LogText") as GameObject;
            go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + Random.Range(1, 2f)),
                Quaternion.identity, _canvasTransform);
            go.GetComponent<Text>().text = "+ 1 дерево";
            
        }
        else if (other.gameObject.CompareTag("Iron"))
        {
            var go = Resources.Load("LogText") as GameObject;
            go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + Random.Range(1, 2f)),
                Quaternion.identity, _canvasTransform);
            go.GetComponent<Text>().text = "+ 1 железо";
            GameManager.gm.iron++;
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Stone"))
        {
            var go = Resources.Load("LogText") as GameObject;
            go = Instantiate(go, new Vector2(transform.position.x, transform.position.y  + Random.Range(1, 2f)),
                Quaternion.identity, _canvasTransform);
            go.GetComponent<Text>().text = "+ 1 камень";
            GameManager.gm.stone++;
            Destroy(other.gameObject);
        }
        
        
    }
    
    
    

    
    
}
