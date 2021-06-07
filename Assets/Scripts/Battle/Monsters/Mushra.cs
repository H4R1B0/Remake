using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mushra : LivingEntity
{
    private int baseHP = 400; //기본 체력
    private int roundHP = 40; //라운드당 추가되는 체력
    private int basePower = 20; //기본 공격력
    private int roundPower = 2; //라운드당 추가되는 공격력

    public Slider HPSliderPrefab; //체력 게이지 프리팹
    private Slider HPSlider; //체력 게이지

    private List<GameObject> FoundObjects;

    void Start()
    {
        //생성시 원래 공격력과 체력 저장
        power = basePower; //공격력
        health = baseHP; //체력
        maxHealth = health;
        //originCritical = critical;

        attackRange = 0.5f; //공격 범위
        attackSpeed = 0.5f; //공격 속도

        //HP, MP 생성
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        //체력 게이지값, 위치 변경
        HPSlider.value = health;
        HPSlider.maxValue = maxHealth;
        //HP
        HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
        HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "공격력 : " + power.ToString();
        HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
    }
}
