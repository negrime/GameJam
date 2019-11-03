using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject player;
    public int wood;
    public int iron;
    public int stone;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else if (gm == this)
        {
            Destroy(gameObject);
        }
    }



}
