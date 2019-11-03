using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField]
    private float xOffset;

    public GameObject[] resources;
    [SerializeField]
    private float _seed;

    [SerializeField]
    private Transform _resourcesPlace;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnResource), 0,_seed);   
    }

    private void SpawnResource()
    {
        Transform p = GameManager.gm.player.transform;
        int number = Random.Range(1, 3) % 2 != 0 ? -1 : 1;
        var k = new Vector3(Random.Range((p.position.x  + 6) * number, 10 * -number ), Random.Range((p.position.y + 6) * number, 10 * -number ), 0);
        
        int resourceChance = Random.Range(1, 101);

        GameObject resource;
        if (resourceChance >= 1 && resourceChance < 60)
        {
            resource = resources[0];
        }
        else if (resourceChance >= 60 && resourceChance < 90)
        {
            resource = resources[1];
        }
        else
        {
            resource = resources[2];
        }
            
            
        var go = Instantiate(resource, k , Quaternion.identity);
        go.gameObject.transform.SetParent(_resourcesPlace);
        go.transform.localEulerAngles = new Vector3(0,0, Random.Range(0, 360));
    }
}
