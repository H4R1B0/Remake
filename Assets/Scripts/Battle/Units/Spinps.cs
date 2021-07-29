using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinps : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    public Slider MPSliderPrefab; //���� ������ ������
    private Slider HPSlider; //ü�� ������
    private Slider MPSlider; //���� ������
    private int level = 1; //���� ����

    private int attackCount;//���� ī��Ʈ

    //public bool isWeapon = true; //���Ⱑ �ִ���
    //public bool isWeaponRotate = true; //���Ⱑ ȸ���ϴ���
    //[ShowIf("isWeapon")] //���� �������� ǥ��
    //public float attackAnimTime = 0; //���� �ִϸ��̼� ��Ÿ��
    public GameObject attackPrefab; //���� ������

    private void Start()
    {
        //������ ���� ���ݷ°� ü�� ����
        originPower = 30; //���� ���ݷ�
        power = originPower; //���ݷ�
        originHealth = 400; //���� ü��
        health = originHealth; //ü��
        maxHealth = health;
        mana = 0;
        //originCritical = critical;

        attackRange = 5; //���� ����
        attackSpeed = 0.9f; //���� �ӵ�

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
        attackCount = 0; //���� ī��Ʈ �ʱ�ȭ
    }
    private void Update()
    {
        //ü�� ��������, ��ġ ����
        HPSlider.value = health;
        MPSlider.value = mana;
        HPSlider.maxValue = maxHealth;

        mana = 0; //�Ʊ� ���� ȹ�� ���� ����

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

        //Ÿ���� �������� �ʾ��� ���
        if (target == null)
        {
            //Debug.Log("Ÿ�� ã��");
            FindMonster();
            animators[0].SetBool("isMove", false);
        }
        //Ÿ���� �׾������
        else if (target.GetComponent<LivingEntity>().IsDie == true)
        {
            target = null;
            //Debug.Log("Ÿ�� ã��");
            FindMonster();
            animators[0].SetBool("isMove", true);
        }
        //Ÿ���� ������ �������� �������� ��Ž��
        else if (target != null && MonsterInCircle() == false)
        {
            animators[0].SetBool("isMove", true);
            FindMonster();
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        //Ÿ���� ���� ���� �ȿ� ���� ���
        else if (MonsterInCircle() == true)
        {
            animators[0].SetBool("isMove", false);

            //StartCoroutine(nameof(AttackAnim));
            //����
            if (isAttack == true)
            {
                //Debug.Log("���� "+Time.time);
                StartCoroutine(nameof(AttackAnim));
                StartCoroutine(nameof(AttackCoroutine));

            }
        }
        //�ʿ� ���Ͱ� �������
        else if (FoundTargets.Count == 0)
        {
            animators[0].SetBool("isMove", false);
        }
    }
    public void OnDestroy()
    {
        Destroy(HPSlider.gameObject);
        Destroy(MPSlider.gameObject);
        Destroy(this.gameObject);
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
                animators[0].SetBool("isMove", false);
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

        yield return null; //���� �ִϸ��̼� ��Ÿ��

        //8/6/4��° ���ݸ��� 40(+1)% �߰� ������
        if (attackCount == 10 - level * 2)
        {
            attackCount = 0;
            GameObject attack = Instantiate(attackPrefab);
            attack.transform.position = this.transform.position + new Vector3(0, -0.8f, 0);
            attack.GetComponent<Attack>().SetPowerDir(power*(13+level)/10, target);

        }
        else
        {
            GameObject attack = Instantiate(attackPrefab);
            attack.transform.position = this.transform.position + new Vector3(0, -0.8f, 0);
            attack.GetComponent<Attack>().SetPowerDir(power, target);
            attackCount++;
        }
        
        //mana += 10; //���ݽ� ���� 10ȹ��        
        animators[0].SetBool("isAttack", false);
    }

    //���� ��Ÿ�� �ڷ�ƾ
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }
}
