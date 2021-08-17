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

        //�Ѿ��� ȭ�� ������ ������� �ı�
        CheckInScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //����
        if(collision.gameObject.tag == "Unit")
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
        }
    }
}