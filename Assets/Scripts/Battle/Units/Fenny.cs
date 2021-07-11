﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fenny : LivingEntity
{
    private List<GameObject> FoundTargets; //찾은 타겟들
    private float shortDis; //타겟들 중에 가장 짧은 거리

    public Slider HPSliderPrefab; //체력 게이지 프리팹
    public Slider MPSliderPrefab; //마나 게이지 프리팹
    private Slider HPSlider; //체력 게이지
    private Slider MPSlider; //마나 게이지
    private int level=1; //유닛 레벨

    //public bool isWeapon = true; //무기가 있는지
    //public bool isWeaponRotate = true; //무기가 회전하는지
    //[ShowIf("isWeapon")] //무기 있을때만 표시
    //public float attackAnimTime = 0; //공격 애니메이션 쿨타임
    //public GameObject attackPrefab; //공격 프리팹

    private void Start()
    {
        //생성시 원래 공격력과 체력 저장
        originPower = 50; //원래 공격력
        power = originPower; //공격력
        originHealth = 500; //원래 체력
        health = originHealth; //체력
        maxHealth = health;
        mana = 0;
        //originCritical = critical;

        attackRange = 4; //공격 범위
        attackSpeed = 0.6f; //공격 속도

        animators = GetComponentsInChildren<Animator>(); //애니메이터들 가져오기

        //HP, MP 생성
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        MPSlider = Instantiate(MPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("MPPosition").position), Quaternion.identity);
        MPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        MPSlider.value = mana;

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //이미지 메테리얼 저장
        renderer = GetComponentInChildren<SpriteRenderer>();

        isAttack = true;
    }
    private void Update()
    {
        //체력 게이지값, 위치 변경
        HPSlider.value = health;
        MPSlider.value = mana;
        HPSlider.maxValue = maxHealth;

        //HP
        HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
        HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "공격력 : " + power.ToString();
        HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
        //MP
        MPSlider.transform.Find("MPCount").GetComponent<Text>().text = MPSlider.value.ToString();
        MPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("MPPosition").position);

        //타겟 향하는
        if (vec3dir.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        //타겟이 정해지지 않았거나 죽었을경우 FindMonster
        if (target == null || target.gameObject.activeSelf == false)
        {
            //Debug.Log("타겟 찾기");
            if (target != null && target.gameObject.activeSelf == false)
            {
                //Beast일경우 적이 죽은 경우 체력 회복
                Debug.Log("타겟 죽음");
            }
            FindMonster();
        }
        //타겟이 공격 범위 안에 있을 경우
        if (MonsterInCircle() == true)
        {
            //마나 100일 경우 스킬 시전
            if (mana >= 100)
            {
                Skill();
                mana = 0;
            }
            animators[0].SetBool("isMove", false);
            //공격
            if (isAttack == true)
            {
                StartCoroutine(nameof(AttackAnim));
                StartCoroutine(nameof(AttackCoroutine));
            }
        }
        //타겟이 있으나 범위에서 벗어났을경우 재탐색
        else if (target != null && MonsterInCircle() == false)
        {
            animators[0].SetBool("isMove", true);
            FindMonster();
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        //맵에 몬스터가 없을경우
        else if(FoundTargets.Count == 0)
        {
            animators[1].SetBool("isAttack", false);
        }
    }

    private void Skill()
    {
        Debug.Log("페니 스킬 시전");
        List<GameObject> FoundMonsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster")); //찾은 모든 몬스터들
        int cnt = FoundMonsters.Count;
        //적이 1명 이상일 경우만
        if (cnt > 0)
        {
            //유닛 레벨당 +1씩 공격할 적 늘어남
            int mnstr = level <= cnt ? level : cnt; //레벨이나 적의 수 중에 작은 값 설정
            for (int i = 0; i < mnstr; i++)
            {
                //기본 공격력의 500% 데미지 크리티컬 false로 공격
                FoundMonsters[i].GetComponent<LivingEntity>().OnDamage(power*5, false); //공격
            }
        }

    }

    //몬스터 찾기
    public void FindMonster()
    {
        //Debug.Log("찾기");
        FoundTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        if (FoundTargets.Count != 0)
        {
            //짧은 거리 찾기
            shortDis = Vector3.Distance(transform.position, FoundTargets[0].transform.position);
            target = FoundTargets[0];
            foreach (GameObject found in FoundTargets)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
                if (Distance < shortDis)
                {
                    target = found;
                    shortDis = Distance;
                }
            }
            vec3dir = target.transform.position - transform.position;
            vec3dir.Normalize();
        }
    }

    //일정한 범위 내에 몬스터 있는지 확인
    public bool MonsterInCircle()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), attackRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Monster")
            {
                return true;
            }

        }
        return false;
    }

    //공격 코루틴
    IEnumerator AttackAnim()
    {
        animators[1].SetBool("isAttack", true);
        vec3dir = target.transform.position - transform.position;
        vec3dir.Normalize();        

        yield return new WaitForSeconds(animators[1].GetFloat("attackTime")); //공격 쿨타임
    
        target.GetComponent<LivingEntity>().OnDamage(power,false); //공격
        mana += 10; //공격시 마나 10획득
        animators[1].SetBool("isAttack", false);
    }

    //공격 쿨타임 코루틴
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }
}
