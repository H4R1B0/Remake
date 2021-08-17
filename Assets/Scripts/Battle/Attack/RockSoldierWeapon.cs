using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSoldierWeapon : Attack
{
    private void Awake()
    {

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.8f, this.transform.position.z);
        int randX = Random.Range(-100, 100);
        int randY = Random.Range(-100, 100);
        vec3dir = new Vector3(randX, randY, 0);
        vec3dir.Normalize();
    }
    void Update()
    {
        //총알이 화면 밖으로 나갈경우 파괴
        CheckInScreen();
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
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
}