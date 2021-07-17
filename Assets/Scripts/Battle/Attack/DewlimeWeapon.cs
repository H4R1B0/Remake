using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DewlimeWeapon : Attack
{
    private Vector3 startPos; //시작하는 위치
    private float preTime; //초기 시간
    private float duration = 0.7f; //속도(낮을수록 빨라짐)
    private float attackRange = 1f; //일정 범위 피해

    private void Awake()
    {
        this.transform.position = new Vector3(this.transform.position.x+0.14f, this.transform.position.y+0.7f, this.transform.position.z);
    }
    private void Start()
    {
        startPos = this.transform.position;
        preTime = Time.time;
    }
    void Update()
    {
        //포물선 이동
        Parabolic(startPos, target.transform.position, (Time.time - preTime) / duration);
        //포물선으로 이동
        //this.transform.position = Vector3.Slerp(transform.position, target.transform.position + (Vector3)target.GetComponent<BoxCollider2D>().offset, 0.02f);
        //transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

        //총알이 화면 밖으로 나갈경우 파괴
        if (this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x)
            Destroy(this.gameObject);
        //Debug.Log(Camera.main.ScreenToWorldPoint(this.transform.position));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("dew 충돌");
        //타겟 충돌시 범위 내에 유닛 공격
        if (collision.collider == target.GetComponent<BoxCollider2D>())
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), attackRange);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Unit")
                {
                    colliders[i].gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
                    //Debug.Log(colliders[i].gameObject.GetInstanceID())
;                }

            }
            
            Destroy(this.gameObject);
        }        
    }
}