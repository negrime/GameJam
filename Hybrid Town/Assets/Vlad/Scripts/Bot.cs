using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Vector2 _movePoint;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotateSpeed;
    void Start()
    {
        _movePoint = NewTarget();
    }

    void Update()
    {
        Vector3 vectorToTarget = _movePoint - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotateSpeed);
        if (Vector2.Distance(transform.position, _movePoint) < 0.2f)
        {
            _movePoint = NewTarget();
        }
        transform.position =  Vector2.MoveTowards(transform.position, _movePoint, _speed * Time.deltaTime);
    }



    void FixedUpdate()
    {

        
        

    }
    private Vector2 NewTarget()
    {
        var pos = GameManager.gm.player.transform.position;
        return new Vector3(Random.Range(pos.x - 15, pos.x + 15), Random.Range(pos.y - 15, pos.y + 15));
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wood"))
        {

            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Iron"))
        {

            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Stone"))
        {
            Destroy(other.gameObject);
        }

        /* if (other.gameObject.CompareTag("Enemy"))
         {
             Debug.Log("Есть контакт");
             other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 250);
         } */
        
    }
}
