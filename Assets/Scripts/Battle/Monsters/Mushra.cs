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
        isDie = false;

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //�̹��� ���׸��� ����
        renderer = GetComponentInChildren<SpriteRenderer>();

        //������ ���� ���ݷ°� ü�� ����
        power = basePower; //���ݷ�
        health = baseHP; //ü��
        maxHealth = health;
        //originCritical = critical;

        attackRange = 0.5f; //���� ����
        attackSpeed = 0.5f; //���� �ӵ�

        animators = GetComponentsInChildren<Animator>(); //�ִϸ����͵� ��������

        //HP, MP ����
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
    }

    void Update()
    {
        //ü�� ��������, ��ġ ����
        HPSlider.value = health;
        HPSlider.maxValue = maxHealth;
        //HP
        //HP
        if (HPSlider != null)
        {
            HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
            HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "���ݷ� : " + power.ToString();
            HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
        }
        

        if (isDie == false && health <= 0)
        {
            isDie = true;
            StartCoroutine(nameof(DestroyCoroutine));
        }
    }

    //�ǰ�
    public override void OnDamage(int damage, bool isCritical)
    {
        if (runningCoroutine != null)
        {
            StartCoroutine(nameof(FlashCoroutine));
        }
        runningCoroutine = StartCoroutine(nameof(FlashCoroutine));
        base.OnDamage(damage, isCritical);
    }

    //�ǰݽ� ���׸��� ���� �޼���
    IEnumerator FlashCoroutine()
    {
        //���� ���׸��� ����
        renderer.material = flashWhite;

        //0.15�ʰ� ���
        yield return new WaitForSeconds(0.15f);

        //���� ���׸��� ����
        renderer.material = defaultMaterial;
    }

    //�׾����� �ڷ�ƾ
    IEnumerator DestroyCoroutine()
    {
        Destroy(HPSlider.gameObject);
        //transform.SetParent(GameManagerTest.instance.deactiveObj.transform);
        animators[0].SetBool("isDie", isDie);
        yield return new WaitForSeconds(animators[0].GetFloat("dieTime")); //�״� ��� �ð�
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}