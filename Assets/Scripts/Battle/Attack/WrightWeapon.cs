using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrightWeapon : Attack
{
    private void Awake()
    {
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        //Debug.Log(this.transform.position);
    }
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        //Debug.Log(vec3dir);
    }
}