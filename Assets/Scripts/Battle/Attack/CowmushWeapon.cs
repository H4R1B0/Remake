using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowmushWeapon : Attack
{
    private void Awake()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-0.2f, this.transform.position.z);
    }
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

        //총알이 화면 밖으로 나갈경우 파괴
        if(this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x)
            Destroy(this.gameObject);
        //Debug.Log(Camera.main.ScreenToWorldPoint(this.transform.position));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //타겟과 충돌시에 공격 및 파괴
        if (collision.collider == target.GetComponent<BoxCollider2D>())
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            //Destroy(this.gameObject);
        }
    }
}