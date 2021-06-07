using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mushra : LivingEntity
{
    private int baseHP = 400; //�⺻ ü��
    private int roundHP = 40; //����� �߰��Ǵ� ü��
    private int basePower = 20; //�⺻ ���ݷ�
    private int roundPower = 2; //����� �߰��Ǵ� ���ݷ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    private Slider HPSlider; //ü�� ������

    private List<GameObject> FoundObjects;

    void Start()
    {
        //������ ���� ���ݷ°� ü�� ����
        power = basePower; //���ݷ�
        health = baseHP; //ü��
        maxHealth = health;
        //originCritical = critical;

        attackRange = 0.5f; //���� ����
        attackSpeed = 0.5f; //���� �ӵ�

        //HP, MP ����
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        //ü�� ��������, ��ġ ����
        HPSlider.value = health;
        HPSlider.maxValue = maxHealth;
        //HP
        HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
        HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "���ݷ� : " + power.ToString();
        HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
    }
}
