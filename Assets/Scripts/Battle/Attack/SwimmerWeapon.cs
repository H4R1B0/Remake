using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmerWeapon : Attack
{
    void Update()
    {
        if (vec3dir.x < 0)
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }

        //총알이 화면 밖으로 나갈경우 파괴
        CheckInScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //타겟과 충돌시에 공격, 관통
        if (collision.collider == target.GetComponent<BoxCollider2D>())
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            //Destroy(this.gameObject);
        }
    }
}
