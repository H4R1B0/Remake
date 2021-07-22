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
        //�Ѿ��� ȭ�� ������ ������� �ı�
        if (this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x //���� ȭ�� �Ѿ��
            || this.transform.position.x > -Camera.main.ScreenToWorldPoint(this.transform.position).x //������ ȭ�� �Ѿ��
            || this.transform.position.y < Camera.main.ScreenToWorldPoint(this.transform.position).y //�Ʒ� ȭ�� �Ѿ��
            || this.transform.position.y > -Camera.main.ScreenToWorldPoint(this.transform.position).y //�� ȭ�� �Ѿ��
           )
        {
            Destroy(this.gameObject);
        }
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ÿ�ٰ� �浹�ÿ� ����, ����
        if (collision.gameObject.tag=="Unit")
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            Destroy(this.gameObject);
        }
    }
}