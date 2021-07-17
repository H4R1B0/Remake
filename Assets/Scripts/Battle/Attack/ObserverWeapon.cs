using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverWeapon : Attack
{
    private void Awake()
    {
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.8f, this.transform.position.z);
    }
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
    }
}