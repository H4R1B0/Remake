using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrightEffect : MonoBehaviour
{
    //private float DestroySec = 0.433f;
    private Animator animator; //�ִϸ����� 

    void Start()
    {
        animator = GetComponent<Animator>(); //�ִϸ����� ��������
        Destroy(this.gameObject, animator.GetFloat("animTime"));
    }
}