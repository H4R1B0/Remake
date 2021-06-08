using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour
{
    protected int power = 30; //공격력
    protected int originPower; //원래 공격력
    protected int health = 50; //체력
    protected int originHealth; //원래 체력
    protected int maxHealth; //최대 체력
    protected int mana; //마나
    protected float attackRange = 1f; //공격 범위
    protected float attackSpeed = 0f; //공격 속도
    //public GameObject DamageText;
    //public bool isStern; //스턴인지
    protected float moveSpeed = 1.3f; // 이동 속도
    protected Vector3 vec3dir = Vector3.right; //움직이는 방향
    protected GameObject target = null; //타겟으로 되는 적
    protected bool isAttack; //공격 가능한지
    protected Animator[] animators; //애니메이터

    public Material flashWhite; //피격시 변경할 메테리얼
    protected Material defaultMaterial; //기본 메테리얼
    protected Coroutine runningCoroutine = null; //실행중인 코루틴
    protected Renderer renderer; //이미지 렌더러

    protected bool isDie; //죽었는지

    //OnDamage 메서드
    public virtual void OnDamage(int damage, bool isCritical)
    {
        
        //GameObject DGText = Instantiate(DamageText, Camera.main.WorldToScreenPoint(transform.Find("Damage").position), Quaternion.identity);
        //DGText.GetComponent<DamageText>().damage = damage;
        if (isCritical == true)
        {
            //DGText.transform.localScale = new Vector3(1.3f, 1.3f, 1);
            //DGText.GetComponent<Text>().color = Color.red;
        }
        health -= damage;
        //mana += 5; //피격시 마나 5획득
    }
}
