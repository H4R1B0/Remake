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

        //�Ѿ��� ȭ�� ������ ������� �ı�
        CheckInScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ÿ�ٰ� �浹�ÿ� ����, ����
        if (collision.collider == target.GetComponent<BoxCollider2D>())
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            //Destroy(this.gameObject);
        }
    }
}
