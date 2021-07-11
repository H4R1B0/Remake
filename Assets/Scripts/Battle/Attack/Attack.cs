using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    protected int power = 0;
    protected float moveSpeed = 8f; // �̵� �ӵ�
    protected Vector3 vec3dir; //�����̴� ����
    protected GameObject target; //Ÿ�� �´��� Ȯ��
    
    public void SetPowerDir(int p, GameObject target)
    {
        power = p;
        this.target = target;
        //Ÿ�� ��ġ�� �ݶ��̴� �߽����� ���Ͽ� Ÿ���� �߽����� ���ϰ� ��
        vec3dir = target.transform.position + (Vector3)target.GetComponent<BoxCollider2D>().offset - transform.position;
        vec3dir.Normalize();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        //Ÿ�ٰ� �浹�ÿ� ���� �� �ı�
        if(collision.collider == target.GetComponent<BoxCollider2D>())
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            Destroy(this.gameObject);
        }
    }
}
