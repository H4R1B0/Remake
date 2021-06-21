using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mushra : LivingEntity
{
    private int baseHP = 700; //기본 체력
    private int roundHP = 40; //라운드당 추가되는 체력
    private int basePower = 20; //기본 공격력
    private int roundPower = 2; //라운드당 추가되는 공격력

    public Slider HPSliderPrefab; //체력 게이지 프리팹
    private Slider HPSlider; //체력 게이지

    private List<GameObject> FoundObjects;

    void Start()
    {
        isDie = false;

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //이미지 메테리얼 저장
        renderer = GetComponentInChildren<SpriteRenderer>();

        //생성시 원래 공격력과 체력 저장
        power = basePower; //공격력
        health = baseHP; //체력
        maxHealth = health;
        //originCritical = critical;

        attackRange = 0.5f; //공격 범위
        attackSpeed = 0.5f; //공격 속도

        animators = GetComponentsInChildren<Animator>(); //애니메이터들 가져오기

        //HP, MP 생성
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        //animators[0].SetBool("isDie", true);

        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //체력 게이지값, 위치 변경
        HPSlider.value = health;
        HPSlider.maxValue = maxHealth;
        //HP
        //HP
        if (HPSlider != null)
        {
            HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
            HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "공격력 : " + power.ToString();
            HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
        }


        if (isDie == false && health <= 0)
        {
            isDie = true;
            StartCoroutine(nameof(DestroyCoroutine));
        }
    }

    //피격
    public override void OnDamage(int damage, bool isCritical)
    {
        
        base.OnDamage(damage, isCritical);
        if (runningCoroutine != null)
        {
            StartCoroutine(nameof(FlashCoroutine));
        }
        else
        {
            runningCoroutine = StartCoroutine(nameof(FlashCoroutine));
        }
    }

    //피격시 메테리얼 변경 메서드
    IEnumerator FlashCoroutine()
    {
        //현재 메테리얼 변경
        renderer.material = FlashWhite;
        //0.15초간 대기
        yield return new WaitForSeconds(0.15f);
        //원래 메테리얼 변경
        renderer.material = defaultMaterial;
    }

    //죽었을때 코루틴
    IEnumerator DestroyCoroutine()
    {
        //플래시 코루틴 멈추고 원래 메테리얼로 복구
        StopCoroutine(nameof(FlashCoroutine));
        renderer.material = defaultMaterial;
        //Debug.Log("FlashCoroutine 멈춤");

        Destroy(HPSlider.gameObject); //체력바 파괴
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
