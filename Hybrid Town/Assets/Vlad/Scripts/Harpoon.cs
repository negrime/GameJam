using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Harpoon : MonoBehaviour
{

    [SerializeField] private float _angle = 45f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Ease ease = Ease.Linear;
    void Start()
    {

        transform
            .DORotate(Vector3.forward * _angle, _duration)
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo);
    }
        



    
}
