using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsyRoomWeapon : Attack
{
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        //총알이 화면 밖으로 나갈경우 파괴
        CheckInScreen();
    }
}