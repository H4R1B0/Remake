using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleStoneWeapon : Attack
{
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

        //총알이 화면 밖으로 나갈경우 파괴
        CheckInScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //타겟과 충돌시에 공격
        if (collision.gameObject.tag == "Unit")
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            Destroy(this.gameObject);
        }
    }

    public void SetPowerDir(int p, Vector3 dir)
    {
        power = p;
        vec3dir = dir; //방향
        vec3dir.Normalize();
    }
}