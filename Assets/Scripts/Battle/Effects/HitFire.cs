using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFire : MonoBehaviour
{
    void Update()
    {
        //첫번째 자식의 자식 개수가 0일때 파괴
        if (transform.GetChild(0).childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
