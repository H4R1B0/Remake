using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wright : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    public Slider MPSliderPrefab; //���� ������ ������
    private Slider HPSlider; //ü�� ������
    private Slider MPSlider; //���� ������
    private int level = 1; //���� ����

    private bool isSkill; //��ų ��� ���� ����

    //public bool isWeapon = true; //���Ⱑ �ִ���
    //public bool isWeaponRotate = true; //���Ⱑ ȸ���ϴ���
    //[ShowIf("isWeapon")] //���� �������� ǥ��
    //public float attackAnimTime = 0; //���� �ִϸ��̼� ��Ÿ��
    public GameObject attackPrefab; //���� ������

    private void Start()
    {
        tribe = "Mammal";

        //������ ���� ���ݷ°� ü�� ����
        originPower = 90; //���� ���ݷ�
        power = originPower; //���ݷ�
        originHealth = 700; //���� ü��
        health = originHealth; //ü��
        maxHealth = health;
        mana = 0;
        //originCritical = critical;

        attackRange = 5f; //���� ����
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
        isStern = false;
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
            animators[0].SetBool("isAttack", false);
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
        //Ÿ�������� �̵�
        else if (target != null && FoundTargets.Count != 0)
        {
            animators[0].SetBool("isMove", true);
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        //�ʿ� ���Ͱ� �������
        else if (FoundTargets.Count == 0)
        {
            animators[0].SetBool("isAttack", false);
        }
    }
    public override void OnDamage(int damage, bool isCritical)
    {
        base.OnDamage(damage, isCritical);
    }
    private void Skill()
    {
        Debug.Log("����Ʈ ��ų ����");
        StartCoroutine(nameof(WrightSkill)); //����Ʈ ��ų ����
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
        animators[0].SetBool("isAttack", true);
        vec3dir = target.transform.position - transform.position;
        vec3dir.Normalize();
        yield return null; //���� ��Ÿ��
        GameObject attack = Instantiate(attackPrefab, this.transform);
        attack.GetComponent<Attack>().SetPowerDir(power, target);
        if (isSkill == false) //��ų ������ �ȵž� ���� ȹ��
            mana += 10; //���ݽ� ���� 10ȹ��
        animators[0].SetBool("isAttack", false);
    }

    //���� ��Ÿ�� �ڷ�ƾ
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }
    //����Ʈ ��ų : 3(+1)�ʰ� ���ݷ�10�� ���ݼӵ� 1%����
    IEnumerator WrightSkill()
    {
        
        float originS = attackSpeed; //���� ���� �ӵ� ����
        attackSpeed = attackSpeed * (100 + power / 10) / 100; //���ݼӵ� ����

        yield return new WaitForSeconds(level+2); //3(+1)�� ��

        attackSpeed = originS; //���� ���� �ӵ�
        isSkill = false;
    }
}