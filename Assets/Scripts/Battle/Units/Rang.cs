using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rang : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    public Slider MPSliderPrefab; //���� ������ ������
    private Slider HPSlider; //ü�� ������
    private Slider MPSlider; //���� ������
    private int level = 1; //���� ����

    private bool isSkill; //��ų ��� ���� ����

    private bool isAlter; //�н�����

    public GameObject rang; //�н� ��

    //public bool isWeapon = true; //���Ⱑ �ִ���
    //public bool isWeaponRotate = true; //���Ⱑ ȸ���ϴ���
    //[ShowIf("isWeapon")] //���� �������� ǥ��
    //public float attackAnimTime = 0; //���� �ִϸ��̼� ��Ÿ��
    //public GameObject attackPrefab; //���� ������
    private void Awake()
    {
        //������ ���� ���ݷ°� ü�� ����
        originPower = 70; //���� ���ݷ�
        power = originPower; //���ݷ�
        originHealth = 700; //���� ü��
        health = originHealth; //ü��
        maxHealth = health;
        mana = 0;
        originCriticalRate = 30; //���� ġ��Ÿ��
        criticalRate = originCriticalRate; //ġ��Ÿ��
        CriticalDamageRate = 130; //ġ��Ÿ ������
        originCriticalDamageRate = CriticalDamageRate; //���� ġ��Ÿ ������

        attackRange = 0.5f; //���� ����
        attackSpeed = 0.7f; //���� �ӵ�

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

        isSkill = false;

        isAlter = false;
    }
    private void Start()
    {
        
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
                isSkill = true;
                Skill();
                mana = 0;
            }
            animators[0].SetBool("isMove", false);
            //����
            if (isAttack == true && isStern == false)
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
        Debug.Log("�� ��ų ����");
        StartCoroutine(nameof(RangSkill)); //�� ��ų ����
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

    //�ı� �Լ�
    public void OnDestroy()
    {
        //base.OnDestroy(); //�ƹ��͵� ����
        Destroy(HPSlider.gameObject);
        Destroy(MPSlider.gameObject);
        //Destroy(this.gameObject);
    }

    //�н� ��ȯ�� ���ݷ�, ü�� ����
    public void SetAlter(int p, int h)
    {
        power = p;
        originPower = power;

        health = h;
        originHealth = health;
        maxHealth = originHealth;

        isSkill = true;
        isAlter = true;
    }

    //���� �ڷ�ƾ
    IEnumerator AttackAnim()
    {
        animators[1].SetBool("isAttack", true);

        yield return new WaitForSeconds(animators[1].GetFloat("attackTime")); //���� �ִϸ��̼� ��Ÿ��

        //ũ��Ƽ��
        int rand = Random.Range(0, 100);
        if (rand >= 0 && rand <= criticalRate)
        {
            target.GetComponent<LivingEntity>().OnDamage(power * CriticalDamageRate / 100, true); //ũ��Ƽ�� ����
        }
        else
        {
            target.GetComponent<LivingEntity>().OnDamage(power, false); //����
        }

        if (isSkill == false && isAlter == false) //�н� ��ų ������ �ȵž� ���� ȹ��, �н��� ���� ȹ�� �Ұ���
            mana += 10; //���ݽ� ���� 10ȹ��

        animators[1].SetBool("isAttack", false);
    }

    //���� ��Ÿ�� �ڷ�ƾ
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }
    //�� ��ų : 8�ʰ� ��ü ������ 40(+10)% 2���� �� ����ǰ ��ȯ
    IEnumerator RangSkill()
    {
        GameObject rang1, rang2;
        rang1 = Instantiate(rang);
        rang1.transform.localScale = new Vector3(transform.localScale.x * 0.6f, transform.localScale.y * 0.6f, transform.localScale.z);
        rang1.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z);
        rang1.GetComponent<Rang>().SetAlter(power * (3 + level) * 10 / 100, maxHealth * (3 + level) * 10 / 100);

        rang2 = Instantiate(rang);
        rang2.transform.localScale = new Vector3(transform.localScale.x * 0.6f, transform.localScale.y * 0.6f, transform.localScale.z);
        rang2.transform.position = new Vector3(transform.position.x - 1f, transform.position.y - 0.5f, transform.position.z);
        rang2.GetComponent<Rang>().SetAlter(power * (3 + level) * 10 / 100, maxHealth * (3 + level) * 10 / 100);

        yield return new WaitForSeconds(8);

        isSkill = false;

        if (rang1 != null)
        {
            //Debug.Log("rang1 �ı�");
            Destroy(rang1);
        }
        if (rang2 != null)
        {
            //Debug.Log("rang2 �ı�");
            Destroy(rang2);
        }

    }
}
