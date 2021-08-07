using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    protected int criticalRate; //치명타율
    protected int originCriticalRate; //원래 치명타율
    protected int CriticalDamageRate; //치명타 피해율
    protected int originCriticalDamageRate; //원래 치명타 피해율
    protected Rigidbody2D rigid; //물리
    public GameObject UIText; //데미지, 힐 텍스트
    public GameObject HealEffect; //힐 이펙트
    public GameObject PoisonEffect; //맹독 이펙트
    public GameObject StatusUpEffect; //스테이터스 향상 이펙트
    public GameObject SternEffect; //스턴 이펙트

    protected string tribe; //종족
    public string Tribe
    {
        get
        {
            return tribe;
        }
    }

    //public GameObject DamageText;
    //public bool isStern; //스턴인지
    protected float moveSpeed = 1.3f; // 이동 속도
    protected Vector3 vec3dir = Vector3.right; //움직이는 방향
    protected GameObject target = null; //타겟으로 되는 적
    protected bool isAttack; //공격 가능한지
    protected bool isStern; //스턴 상태
    protected Animator[] animators; //애니메이터

    public Material FlashWhite; //피격시 변경할 메테리얼
    protected Material defaultMaterial; //기본 메테리얼
    protected Coroutine runningCoroutine = null; //실행중인 코루틴
    protected new Renderer renderer; //이미지 렌더러

    protected bool isDie; //죽었는지
    public bool IsDie
    {
        get
        {
            return isDie;
        }
    }

    //OnDamage 메서드
    public virtual void OnDamage(int damage, bool isCritical)
    {


        if (isCritical == true)
        {
            //데미지 표시
            //DGText.transform.localScale = new Vector3(1.3f, 1.3f, 1);
            //DGText.GetComponent<Text>().color = Color.red;
        }
        health -= damage;

        GameObject DamageText = Instantiate(UIText, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1, 0)), Quaternion.identity);
        DamageText.GetComponent<UIText>().Number = damage;

        //mana += 5; //피격시 마나 5획득
        if (runningCoroutine != null)
        {
            StartCoroutine(nameof(FlashCoroutine));
        }
        else
        {
            runningCoroutine = StartCoroutine(nameof(FlashCoroutine));
        }
    }

    //체력 count만큼 회복
    public void HealHP(int count)
    {
        Instantiate(HealEffect, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        GameObject HealText = Instantiate(UIText, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 0.8f, 0)), Quaternion.identity);
        HealText.GetComponent<UIText>().Number = count;
        HealText.GetComponent<TextMeshProUGUI>().color = Color.green;

        health = maxHealth > health + count ? health + count : maxHealth;
    }

    //마나 count만큼 회복
    public void HealMP(int count)
    {
        mana = 100 > mana + count ? mana + count : 100;
    }

    public void Knockback(Vector2 pos)
    {
        int reaction = transform.position.x - pos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(reaction, 1) * 10, ForceMode2D.Impulse); //넉백 정도
    }

    //출혈 코루틴 : time초간 매 초 damage 피해
    public IEnumerator BleedingCoroutine(int time, int damage, string attribute = "")
    {
        //맹독 이펙트
        if (attribute == "poison")
        {
            GameObject poisonEffect = Instantiate(PoisonEffect, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            poisonEffect.GetComponent<ParticleSystem>().Stop(); //재생중에는 시간 변경 못함
            var main = poisonEffect.GetComponent<ParticleSystem>().main;
            main.duration = time;
            poisonEffect.GetComponent<ParticleSystem>().Play();
        }
        for (int i = 0; i < time; i++)
        {
            OnDamage(damage, false);
            Debug.Log(damage + " 피해");
            yield return new WaitForSeconds(1); //1초 쿨
        }
    }

    //time초간 스턴 코루틴
    public IEnumerator SternCoroutine(int time)
    {
        Debug.Log(time + " 초 간 스턴");
        isStern = true;

        //스턴
        GameObject stern = Instantiate(SternEffect, this.transform.position + new Vector3(0, 0.15f, 0), Quaternion.identity);
        stern.GetComponent<Stern>().DestroySec = time;
        animators[0].speed = 0; //멈춤

        yield return new WaitForSeconds(time); //time초 쿨

        animators[0].speed = 1; //다시 재생
        isStern = false;

    }

    //일정 시간동안 공격력 증가 코루틴
    public IEnumerator IncreasingPowerCoroutine(int powercnt, int time) //증가하는 공격력량, 증가하는 시간
    {
        int origin = power;
        power += powercnt;
        yield return new WaitForSeconds(time);
        power = origin;
    }

    //몇초간 count만큼 체력 회복하는 코루틴
    public IEnumerator IncreasingHPCoroutine(int time, int count) //시간, 회복하는 체력량
    {
        for (int i = 0; i < time; i++)
        {
            HealHP(count);
            yield return new WaitForSeconds(1);
        }
    }
    //피격시 메테리얼 변경 메서드
    protected IEnumerator FlashCoroutine()
    {
        //현재 메테리얼 변경
        renderer.material = FlashWhite;
        //0.15초간 대기
        yield return new WaitForSeconds(0.15f);
        //원래 메테리얼 변경
        renderer.material = defaultMaterial;
    }
}
