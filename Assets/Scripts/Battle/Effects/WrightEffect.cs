using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrightEffect : MonoBehaviour
{
    //private float DestroySec = 0.433f;
    private Animator animator; //애니메이터 

    void Start()
    {
        animator = GetComponent<Animator>(); //애니메이터 가져오기
        Destroy(this.gameObject, animator.GetFloat("animTime"));
    }
}