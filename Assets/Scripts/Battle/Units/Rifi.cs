using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifi : Unit
{
    public GameObject attackPrefab; //공격 프리팹

    private void Awake()
    {
        //생성시 원래 공격력과 체력 저장
        originPower = 40; //원래 공격력
        power = originPower; //공격력
        originHealth = 800; //원래 체력
        health = originHealth; //체력
        maxHealth = health;
        mana = 0;

        originCriticalRate = 10; //원래 치명타율
        criticalRate = originCriticalRate; //치명타율
        CriticalDamageRate = 130; //치명타 피해율
        originCriticalDamageRate = CriticalDamageRate; //원래 치명타 피해율

        attackRange = 4; //공격 범위
        attackSpeed = 0.5f; //공격 속도

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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

        //게임 시작
        if (GameManager.instance.IsStart == true)
        {
            //타겟이 정해지지 않았을 경우
            if (target == null)
            {
                //Debug.Log("타겟 찾기");
                FindMonster();
                animators[0].SetBool("isMove", false);
            }
            //타겟이 죽었을경우
            else if (target.GetComponent<LivingEntity>().IsDie == true)
            {
                target = null;
                //Debug.Log("타겟 찾기");
                FindMonster();
                animators[0].SetBool("isMove", true);
            }
            //타겟이 있으나 범위에서 벗어났을경우 재탐색
            else if (target != null && MonsterInCircle() == false)
            {
                animators[0].SetBool("isMove", true);
                FindMonster();
                transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
            }
            //타겟이 공격 범위 안에 있을 경우
            else if (MonsterInCircle() == true)
            {
                animators[0].SetBool("isMove", false);

                //마나 100일 경우 스킬 시전
                if (mana >= 100)
                {
                    Skill();
                    mana = 0;
                }
                //StartCoroutine(nameof(AttackAnim));
                //공격
                if (isAttack == true && isStern == false)
                {
                    //Debug.Log("공격 "+Time.time);
                    StartCoroutine(nameof(AttackAnim));
                    StartCoroutine(nameof(AttackCoroutine));

                }
            }
        }
        //게임 시작 전 이거나 게임 종료 
        else
        {
            health = maxHealth; //최대 체력으로 회복
            mana = 0; //마나 초기화

            animators[0].SetBool("isMove", false);
        }
    }
    public void OnDestroy()
    {
        Destroy(HPSlider.gameObject);
        Destroy(MPSlider.gameObject);
        //Destroy(this.gameObject);
    }

    private void Skill()
    {
        Debug.Log("리피 스킬 시전");
        StartCoroutine(nameof(RifiSkill)); //리피 스킬 시전
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
                animators[0].SetBool("isMove", false);
                return true;
            }

        }
        return false;
    }
    public override void OnDamage(int damage, bool isCritical)
    {
        base.OnDamage(damage - guardiansSynergyReducedDamage, isCritical);

        //체력이 0보다 작을경우 비활성화
        if (health <= 0)
        {
            StopAllCoroutines();
            isAttack = true;
            health = maxHealth;
            mana = 0;
            spriteRenderer.material = defaultMaterial;
            GameObject disabledObjects = GameObject.Find("DisabledObjects"); //비활성화 관리하는 오브젝트
            transform.SetParent(disabledObjects.transform);
            HPSlider.transform.SetParent(disabledObjects.transform);
            MPSlider.transform.SetParent(disabledObjects.transform);
            HPSlider.gameObject.SetActive(false);
            MPSlider.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    //공격 코루틴
    IEnumerator AttackAnim()
    {
        animators[0].SetBool("isAttack", true);
        vec3dir = target.transform.position - transform.position;
        vec3dir.Normalize();

        yield return null; //공격 쿨타임

        GameObject attack = Instantiate(attackPrefab);
        attack.transform.position = this.transform.position + new Vector3(0, -0.8f, 0);
        //크리티컬
        int rand = Random.Range(0, 100);
        if (rand >= 0 && rand <= criticalRate)
        {
            attack.GetComponent<Attack>().SetPowerDir(power * CriticalDamageRate / 100, target); //크리티컬 공격
        }
        else
        {
            attack.GetComponent<Attack>().SetPowerDir(power, target);
        }

        mana += 10; //공격시 마나 10획득        
        animators[0].SetBool("isAttack", false);
    }

    //공격 쿨타임 코루틴
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }
    //리피 스킬 : 5초간 매초마다 주변 적에게 10/20/40의 고정피해를 입히고 맹독을 적용합니다
    IEnumerator RifiSkill()
    {
        GameObject[] foundMonsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject foundMonster in foundMonsters)
        {
            StartCoroutine(foundMonster.GetComponent<LivingEntity>().BleedingCoroutine(5, unitLevel * 10, "poison"));
        }
        yield return null;
    }
}