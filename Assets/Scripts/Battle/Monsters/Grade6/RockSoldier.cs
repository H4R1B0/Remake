using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockSoldier : Monster
{
    public GameObject attackPrefab; //공격 프리팹

    private void Awake()
    {
        baseHP = 400; //기본 체력
        roundHP = 50; //라운드당 추가되는 체력
        basePower = 30; //기본 공격력
        roundPower = 4; //라운드당 추가되는 공격력

        isDie = false;

        vec3dir = Vector3.up;
        moveSpeed *= 1.5f; //이동속도 빠르게

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //이미지 메테리얼 저장
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        //생성시 원래 공격력과 체력 저장
        power = basePower + roundPower * (GameManager.instance.Round - 1); //공격력
        health = baseHP + roundHP * (GameManager.instance.Round - 1); //체력
        maxHealth = health;
        //originCritical = critical;

        attackRange = 10f; //공격 범위
        attackSpeed = 1.2f; //공격 속도

        animators = GetComponentsInChildren<Animator>(); //애니메이터들 가져오기

        //HP, MP 생성
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        //animators[0].SetBool("isDie", true);

        rigid = GetComponent<Rigidbody2D>();

        isAttack = true;
    }

    void Update()
    {
        //체력 게이지값, 위치 변경
        HPSlider.value = health;
        HPSlider.maxValue = maxHealth;
        //HP
        if (HPSlider != null)
        {
            HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
            HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "공격력 : " + power.ToString();
            HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
        }

        //상하 이동
        if (this.transform.position.y + this.GetComponent<BoxCollider2D>().offset.y + this.GetComponent<BoxCollider2D>().size.y / 2 > -Camera.main.ScreenToWorldPoint(this.transform.position).y) //위쪽 화면 넘어갈때
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.down;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

            //Debug.Log(vec3dir);
        }
        else if (this.transform.position.y + this.GetComponent<BoxCollider2D>().offset.y - this.GetComponent<BoxCollider2D>().size.y / 2 < Camera.main.ScreenToWorldPoint(this.transform.position).y) //아래쪽 화면 넘어갈때
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.up;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

            //Debug.Log(vec3dir);
        }

        else
        {
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }

        if (isAttack == true && isDie == false)
        {
            StartCoroutine(nameof(AttackAnim));
            StartCoroutine(nameof(AttackCoroutine));
        }
    }

    //피격
    public override void OnDamage(int damage, bool isCritical)
    {
        base.OnDamage(damage, isCritical);

        //체력이 0보다 작을경우 파괴
        if (health <= 0)
        {
            isDie = true;
            StartCoroutine(nameof(DestroyCoroutine));
            moveSpeed = 0;
        }
    }

    public void FindUnit()
    {
        FoundTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        if (FoundTargets.Count != 0)
        {
            shortDis = Vector3.Distance(transform.position, FoundTargets[0].transform.position); // 첫번째를 기준으로 잡아주기 

            target = FoundTargets[0]; // 첫번째를 먼저 
            foreach (GameObject found in FoundTargets)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    target = found;
                    shortDis = Distance;
                }
            }
            vec3dir = (target.transform.position - new Vector3(0, 1f, 0)) - transform.position;
            //vec3dir = target.transform.position - transform.position;
            vec3dir.Normalize();
        }

    }
    public bool UnitInCircle()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), attackRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Unit")
            {
                return true;
            }

        }
        return false;
    }
    public void OnDestroy()
    {
        Destroy(HPSlider.gameObject);
    }
    //공격 코루틴
    IEnumerator AttackAnim()
    {
        animators[0].SetBool("isAttack", true);
        yield return new WaitForSeconds(animators[0].GetFloat("attackTime")); //공격 애니메이션 타임

        //Debug.Log(start + "\n" + end);
        for (int i = 0; i < 5; i++)
        {
            GameObject attack = Instantiate(attackPrefab);
            attack.transform.position = this.transform.position;
            attack.GetComponent<Attack>().SetPower(power);
        }

        animators[0].SetBool("isAttack", false);
    }

    //공격 쿨타임 코루틴
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }

    //죽었을때 코루틴
    IEnumerator DestroyCoroutine()
    {
        //플래시 코루틴 멈추고 원래 메테리얼로 복구
        StopCoroutine(nameof(FlashCoroutine));
        spriteRenderer.material = defaultMaterial;
        //Debug.Log("FlashCoroutine 멈춤");

        //Destroy(HPSlider.gameObject); //체력바 파괴
        animators[0].SetBool("isDie", isDie); //isDie로 애니메이션 실행
        yield return new WaitForSeconds(animators[0].GetFloat("dieTime")); //죽는 모션
        animators[0].speed = 0; //죽은 후에 애니메이션 멈춤
        //Debug.Log(animators[0].GetBool("isDie"));

        StartCoroutine(nameof(FadeoutCoroutine)); //죽을때 페이드아웃
        //yield return new WaitForSeconds(animators[0].GetFloat("dieTime")); //죽는 모션 시간
        yield return new WaitForSeconds(1); //1초


        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    //이미지 페이드아웃
    IEnumerator FadeoutCoroutine()
    {
        for (float i = 1f; i > 0; i -= 0.1f)
        {
            spriteRenderer.material.color = new Vector4(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}