using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleStoneWeapon : Attack
{
    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

        //�Ѿ��� ȭ�� ������ ������� �ı�
        CheckInScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ÿ�ٰ� �浹�ÿ� ����
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