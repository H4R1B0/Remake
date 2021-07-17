using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    protected int power = 0;
    protected float moveSpeed = 8f; // 이동 속도
    protected Vector3 vec3dir; //움직이는 방향
    protected GameObject target; //타겟 맞는지 확인
    
    public void SetPowerDir(int p, GameObject target)
    {
        power = p;
        this.target = target;
        //타겟 위치에 콜라이더 중심점을 더하여 타겟의 중심으로 향하게 함
        vec3dir = target.transform.position + (Vector3)target.GetComponent<BoxCollider2D>().offset - transform.position;
        vec3dir.Normalize();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        //타겟과 충돌시에 공격 및 파괴
        if(collision.collider == target.GetComponent<BoxCollider2D>())
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            Destroy(this.gameObject);
        }
    }

    //참조 - https://answers.unity.com/questions/1515853/move-from-a-to-b-using-parabola-with-or-without-it.html
    protected void Parabolic(Vector3 start, Vector3 end, float time)
    {
        float heigh = 3;
        float target_X = start.x + (end.x - start.x) * time;
        //float maxHeigh = (a.y + b.y) / 2 + heigh;
        float target_Y = start.y + (end.y - start.y) * time + heigh * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        this.transform.position = new Vector3(target_X, target_Y);
    }
}
