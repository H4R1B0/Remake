using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleStoneWeapon : Attack
{
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

        //�Ѿ��� ȭ�� ������ ������� �ı�
        if (this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x //���� ȭ�� �Ѿ��
            || this.transform.position.x > -Camera.main.ScreenToWorldPoint(this.transform.position).x //������ ȭ�� �Ѿ��
            || this.transform.position.y < Camera.main.ScreenToWorldPoint(this.transform.position).y //�Ʒ� ȭ�� �Ѿ��
            || this.transform.position.y > -Camera.main.ScreenToWorldPoint(this.transform.position).y //�� ȭ�� �Ѿ��
           )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ÿ�ٰ� �浹�ÿ� ����, ����
        if (collision.gameObject.tag == "Unit")
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            Destroy(this.gameObject);
        }
    }

    public void SetPowerDir(int p, Vector3 dir)
    {
        power = p;
        vec3dir = dir; //����
        vec3dir.Normalize();
    }
}