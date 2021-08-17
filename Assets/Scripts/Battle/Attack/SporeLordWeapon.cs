using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeLordWeapon : Attack
{
    private void Awake()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        //총알이 화면 밖으로 나갈경우 파괴
        CheckInScreen();
    }
}
