using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsyRoomWeapon : Attack
{
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
    }
}