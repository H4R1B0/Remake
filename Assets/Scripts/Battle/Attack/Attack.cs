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

    //���� - https://answers.unity.com/questions/1515853/move-from-a-to-b-using-parabola-with-or-without-it.html
    protected void Parabolic(Vector3 start, Vector3 end, float time)
    {
        float heigh = 3;
        float target_X = start.x + (end.x - start.x) * time;
        //float maxHeigh = (a.y + b.y) / 2 + heigh;
        float target_Y = start.y + (end.y - start.y) * time + heigh * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        this.transform.position = new Vector3(target_X, target_Y);
    }
}
