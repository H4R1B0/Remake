using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class Baroque : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    public Slider MPSliderPrefab; //���� ������ ������
    private Slider HPSlider; //ü�� ������
    private Slider MPSlider; //���� ������
    private int level = 1; //���� ����

    //public bool isWeapon = true; //���Ⱑ �ִ���
    //public bool isWeaponRotate = true; //���Ⱑ ȸ���ϴ���
    //[ShowIf("isWeapon")] //���� �������� ǥ��
    //public float attackAnimTime = 0; //���� �ִϸ��̼� ��Ÿ��
    //public GameObject attackPrefab; //���� ������

    private void Start()
    {
        //������ ���� ���ݷ°� ü�� ����
        originPower = 60; //���� ���ݷ�
        power = originPower; //���ݷ�
        originHealth = 600; //���� ü��
        health = originHealth; //ü��
        maxHealth = health;
        mana = 0;
        //originCritical = critical;

        attackRange = 0.5f; //���� ����
        attackSpeed = 0.8f; //���� �ӵ�

        animators = GetComponentsInChildren<Animator>(); //�ִϸ����͵� ��������

        //HP, MP ����
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        MPSlider = Instantiate(MPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("MPPosition").position), Quaternion.identity);
        MPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        MPSlider.value = mana;

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //�̹��� ���׸��� ����
        renderer = GetComponentInChildren<SpriteRenderer>();

        isAttack = true;
    }
    private void Update()
    {
        //ü�� ��������, ��ġ ����
        HPSlider.value = health;
        MPSlider.value = mana;
        HPSlider.maxValue = maxHealth;

        //HP
        HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
        HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "���ݷ� : " + power.ToString();
        HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
        //MP
        MPSlider.transform.Find("MPCount").GetComponent<Text>().text = MPSlider.value.ToString();
        MPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("MPPosition").position);

        //Ÿ�� ���ϴ�
        if (vec3dir.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        //Ÿ���� �������� �ʾҰų� �׾������ FindMonster
        if (target == null || target.GetComponent<LivingEntity>().IsDie == true)
        {
            animators[1].SetBool("isAttack", false);
            //Debug.Log("Ÿ�� ã��");
            FindMonster();
        }
        //Ÿ���� ���� ���� �ȿ� ���� ���
        else if (MonsterInCircle() == true)
        {
            //���� 100�� ��� ��ų ����
            if (mana >= 100)
            {
                Skill();
                mana = 0;
            }
            animators[0].SetBool("isMove", false);
            //����
            if (isAttack == true)
            {
                StartCoroutine(nameof(AttackAnim));
                StartCoroutine(nameof(AttackCoroutine));
            }
        }
        //Ÿ���� ������ �������� �������� ��Ž��
        else if (target != null && MonsterInCircle() == false)
        {
            animators[0].SetBool("isMove", true);
            FindMonster();
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        //�ʿ� ���Ͱ� �������
        else if (FoundTargets.Count == 0)
        {
            animators[1].SetBool("isAttack", false);
        }
    }

    private void Skill()
    {
        Debug.Log("�ٷ�ũ ��ų ����");
        StartCoroutine(nameof(BaroqueSkill)); //�ٷ�ũ ��ų ����
    }

    //���� ã��
    public void FindMonster()
    {
        //Debug.Log("ã��");
        FoundTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        if (FoundTargets.Count != 0)
        {
            //ª�� �Ÿ� ã��
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

    //������ ���� ���� ���� �ִ��� Ȯ��
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

    //���� �ڷ�ƾ
    IEnumerator AttackAnim()
    {
        animators[1].SetBool("isAttack", true);
        mana += 10; //���ݽ� ���� 10ȹ��
        yield return new WaitForSeconds(animators[1].GetFloat("attackTime")); //���� ��Ÿ��
        // ���Ÿ�        
        target.GetComponent<LivingEntity>().OnDamage(power, false); //����
        //GameObject attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        //vec3dir = target.transform.position - transform.position;
        //vec3dir.Normalize();
        //attack.GetComponent<Attack>().target = target;
        //attack.GetComponent<Bullet>().setDir(vec3dir);
        ////ũ��Ƽ��
        //int rand = Random.Range(0, 100);
        //if (rand >= 0 && rand <= critical)
        //{
        //    attack.GetComponent<Attack>().isCritical = true;
        //    attack.GetComponent<Bullet>().Power = power * 2;
        //}
        //else
        //{
        //    attack.GetComponent<Attack>().isCritical = false;
        //    attack.GetComponent<Bullet>().Power = power;
        //}
        animators[1].SetBool("isAttack", false);
    }

    //���� ��Ÿ�� �ڷ�ƾ
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }
    //�ٷ�ũ ��ų : ü���� 200/400/800 ȸ���ϰ� ���ݷ��� 10/20/40�����մϴ� -> 10�ʰ�
    IEnumerator BaroqueSkill()
    {
        //ü�� ȸ��
        health = maxHealth > health + (int)(Mathf.Pow(2, level - 1)) * 200 ? health + (int)(Mathf.Pow(2, level - 1)) * 200 : maxHealth;

        //10�ʰ� ���ݷ� 2^(level-1)*10 ��ŭ ����
        power += (int)(Mathf.Pow(2, level - 1)) * 10;
        yield return new WaitForSeconds(10); //10�ʰ� ����
        power -= (int)(Mathf.Pow(2, level - 1)) * 10;
    }
}
