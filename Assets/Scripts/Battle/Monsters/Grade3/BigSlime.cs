using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigSlime : LivingEntity
{
    private List<GameObject> FoundTargets; //찾은 타겟들
    private float shortDis; //타겟들 중에 가장 짧은 거리

    private int baseHP = 400; //기본 체력
    private int roundHP = 50; //라운드당 추가되는 체력
    private int basePower = 30; //기본 공격력
    private int roundPower = 4; //라운드당 추가되는 공격력

    public Slider HPSliderPrefab; //체력 게이지 프리팹
    private Slider HPSlider; //체력 게이지

    public GameObject Slime; //죽을때 소환할 슬라임

    void Start()
    {
        isDie = false;

        moveSpeed *= 1.5f; //이동속도 빠르게

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //이미지 메테리얼 저장
        renderer = GetComponentInChildren<SpriteRenderer>();

        //생성시 원래 공격력과 체력 저장
        power = basePower + roundPower * (GameManager.instance.Round - 1); //공격력
        health = baseHP + roundHP * (GameManager.instance.Round - 1); //체력
        maxHealth = health;
        //originCritical = critical;

        //attackRange = 5f; //공격 범위
        //attackSpeed = 0.5f; //공격 속도
        vec3dir = Vector3.left; //기본적으로 왼쪽으로

        animators = GetComponentsInChildren<Animator>(); //애니메이터들 가져오기

        //HP, MP 생성
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        //animators[0].SetBool("isDie", true);

        rigid = GetComponent<Rigidbody2D>();

        //isAttack = true;
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

        //좌우 이동
        if (this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x+this.GetComponent<BoxCollider2D>().size.x/2) //왼쪽 화면 넘어갈때
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.right;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

            //Debug.Log(vec3dir);
        }
        else if (this.transform.position.x > -Camera.main.ScreenToWorldPoint(this.transform.position).x - this.GetComponent<BoxCollider2D>().size.x / 2) //오른쪽 화면 넘어갈때
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.left;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
        }
    }
    public void OnDestroy()
    {
        Destroy(HPSlider.gameObject);
    }

    //죽었을때 코루틴
    IEnumerator DestroyCoroutine()
    {
        //플래시 코루틴 멈추고 원래 메테리얼로 복구
        StopCoroutine(nameof(FlashCoroutine));
        renderer.material = defaultMaterial;
        //Debug.Log("FlashCoroutine 멈춤");

        //죽을때 슬라임 8마리 소환
        GameObject[] slimes = new GameObject[8];
        for(int i = 0; i < 8; i++)
        {
            slimes[i] = Instantiate(Slime);
            slimes[i].transform.position = this.transform.position - new Vector3(0.3f*i,0,0);
        }

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
            renderer.material.color = new Vector4(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}