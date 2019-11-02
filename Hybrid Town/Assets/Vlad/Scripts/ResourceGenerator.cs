using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField]
    private float xOffset;

    public GameObject[] resources;
    private Vector2 seed;
 
    void Update()
    {
        Transform p = GameManager.gm.player.transform;
        p.position = new Vector3(p.position.x + 10, p.position.y + 10, 0);

        Instantiate(resources[ Random.Range(0, resources.Length - 1)], p.position  , Quaternion.identity);
        
    }
}
