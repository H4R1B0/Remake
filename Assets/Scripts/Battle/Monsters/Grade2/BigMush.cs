using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigMush : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    private int baseHP = 700; //�⺻ ü��
    private int roundHP = 50; //����� �߰��Ǵ� ü��
    private int basePower = 40; //�⺻ ���ݷ�
    private int roundPower = 3; //����� �߰��Ǵ� ���ݷ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    private Slider HPSlider; //ü�� ������

    void Start()
    {
        isDie = false;

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //�̹��� ���׸��� ����
        renderer = GetComponentInChildren<SpriteRenderer>();

        //������ ���� ���ݷ°� ü�� ����
        power = basePower + roundPower * (GameManager.instance.Round - 1); //���ݷ�
        health = baseHP + roundHP * (GameManager.instance.Round - 1); //ü��
        maxHealth = health;
        //originCritical = critical;

        attackRange = 0.5f; //���� ����
        attackSpeed = 0.4f; //���� �ӵ�

        animators = GetComponentsInChildren<Animator>(); //�ִϸ����͵� ��������

        //HP, MP ����
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

        //Ÿ�� ���ϴ�
        if (vec3dir.x >= 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        //Ÿ���� �������� �ʾҰų� �׾������ FindUnit
        if (target == null || target.GetComponent<LivingEntity>().IsDie == true)
        {
            animators[0].SetBool("isAttack", false);
            //Debug.Log("Ÿ�� ã��");
            FindUnit();
        }
        //Ÿ���� ���� ���� �ȿ� ���� ���
        else if (UnitInCircle() == true)
        {
            //animators[0].SetBool("isMove", false);
            //����
            if (isAttack == true && isDie == false)
            {
                StartCoroutine(nameof(AttackAnim));
                StartCoroutine(nameof(AttackCoroutine));
            }
        }
        //Ÿ�������� �̵�
        else if (target != null && UnitInCircle() == false)
        {
            FindUnit();
            animators[0].SetBool("isAttack", false);
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        //�ʿ� ������ �������
        else if (FoundTargets.Count == 0)
        {
            animators[0].SetBool("isAttack", false);
        }
    }

    //�ǰ�
    public override void OnDamage(int damage, bool isCritical)
    {
        base.OnDamage(damage, isCritical);

        //ü���� 0���� ������� �ı�
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
            shortDis = Vector3.Distance(transform.position, FoundTargets[0].transform.position); // ù��°�� �������� ����ֱ� 

            target = FoundTargets[0]; // ù��°�� ���� 
            foreach (GameObject found in FoundTargets)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis) // ������ ���� �������� �Ÿ� ���
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
    //���� �ڷ�ƾ
    IEnumerator AttackAnim()
    {
        animators[0].SetBool("isAttack", true);
        yield return new WaitForSeconds(animators[0].GetFloat("attackTime")); //���� �ִϸ��̼� Ÿ��

        int rand = Random.Range(0, 100);
        //15%Ȯ���� 1�� ���� 
        if (rand >= 0 && rand < 15)
            StartCoroutine(target.GetComponent<LivingEntity>().SternCoroutine(1));

        //��� �� ����
        GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach(GameObject foundUnit in foundUnits)
        {
            foundUnit.GetComponent<LivingEntity>().OnDamage(power, false); //����
        }
        
        animators[0].SetBool("isAttack", false);
    }

    //���� ��Ÿ�� �ڷ�ƾ
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }



    //�׾����� �ڷ�ƾ
    IEnumerator DestroyCoroutine()
    {
        //�÷��� �ڷ�ƾ ���߰� ���� ���׸���� ����
        StopCoroutine(nameof(FlashCoroutine));
        renderer.material = defaultMaterial;
        //Debug.Log("FlashCoroutine ����");

        //Destroy(HPSlider.gameObject); //ü�¹� �ı�
        animators[0].SetBool("isDie", isDie); //isDie�� �ִϸ��̼� ����
        yield return new WaitForSeconds(animators[0].GetFloat("dieTime")); //�״� ���
        animators[0].speed = 0; //���� �Ŀ� �ִϸ��̼� ����
        //Debug.Log(animators[0].GetBool("isDie"));

        StartCoroutine(nameof(FadeoutCoroutine)); //������ ���̵�ƿ�
        //yield return new WaitForSeconds(animators[0].GetFloat("dieTime")); //�״� ��� �ð�
        yield return new WaitForSeconds(1); //1��

        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    //�̹��� ���̵�ƿ�
    IEnumerator FadeoutCoroutine()
    {
        for (float i = 1f; i > 0; i -= 0.1f)
        {
            renderer.material.color = new Vector4(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}