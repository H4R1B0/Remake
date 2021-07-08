using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DewlimeWeapon : Attack
{
    private float attackRange = 1f; //���� ���� ����
    private void Awake()
    {
        this.transform.position = new Vector3(this.transform.position.x+0.14f, this.transform.position.y+0.7f, this.transform.position.z);
    }
    void Update()
    {
        //���������� �̵�
        this.transform.position = Vector3.Slerp(transform.position, target.transform.position + (Vector3)target.GetComponent<BoxCollider2D>().offset, 0.02f);
        //transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

        //�Ѿ��� ȭ�� ������ ������� �ı�
        if (this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x)
            Destroy(this.gameObject);
        //Debug.Log(Camera.main.ScreenToWorldPoint(this.transform.position));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("dew �浹");
        //Ÿ�� �浹�� ���� ���� ���� ����
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