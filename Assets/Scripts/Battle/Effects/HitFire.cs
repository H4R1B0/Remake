using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFire : MonoBehaviour
{
    void Update()
    {
        //ù��° �ڽ��� �ڽ� ������ 0�϶� �ı�
        if (transform.GetChild(0).childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
