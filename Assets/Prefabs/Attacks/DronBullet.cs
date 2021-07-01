using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronBullet : Attack
{
    private void Awake()
    {
        moveSpeed = 9f; // 이동 속도 수정
        this.transform.position = new Vector3(this.transform.position.x-0.36f, this.transform.position.y-0.649f, this.transform.position.z);
    }
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
    }
}
