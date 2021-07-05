using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    protected int power = 0;
    protected float moveSpeed = 8f; // �̵� �ӵ�
    protected Vector3 vec3dir; //�����̴� ����
    
    public void SetPowerDir(int p, GameObject target)
    {
        power = p;
        vec3dir = target.transform.position - transform.position;
        vec3dir.Normalize();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("����");
        collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
        Destroy(this.gameObject);
    }
}
