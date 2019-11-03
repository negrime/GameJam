using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Harpoon : MonoBehaviour
{
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("happon", 0);
    }

    private void Update()
    {
        
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
         //   Debug.Log("Ke");
           SceneManager.LoadScene("Fight");
        }
    }
}
