using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockSoldier : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    private int baseHP = 400; //�⺻ ü��
    private int roundHP = 50; //����� �߰��Ǵ� ü��
    private int basePower = 30; //�⺻ ���ݷ�
    private int roundPower = 4; //����� �߰��Ǵ� ���ݷ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    private Slider HPSlider; //ü�� ������

    public GameObject attackPrefab; //���� ������

    private void Awake()
    {
        isDie = false;

        vec3dir = Vector3.up;
        moveSpeed *= 1.5f; //�̵��ӵ� ������

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //�̹��� ���׸��� ����
        renderer = GetComponentInChildren<SpriteRenderer>();

        //������ ���� ���ݷ°� ü�� ����
        power = basePower + roundPower * (GameManager.instance.Round - 1); //���ݷ�
        health = baseHP + roundHP * (GameManager.instance.Round - 1); //ü��
        maxHealth = health;
        //originCritical = critical;

        attackRange = 10f; //���� ����
        attackSpeed = 1.2f; //���� �ӵ�

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
        if (HPSlider != null)
        {
            HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
            HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "���ݷ� : " + power.ToString();
            HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);
        }

        //���� �̵�
        if (this.transform.position.y + this.GetComponent<BoxCollider2D>().offset.y+this.GetComponent<BoxCollider2D>().size.y/2 > -Camera.main.ScreenToWorldPoint(this.transform.position ).y) //���� ȭ�� �Ѿ��
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.down;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

            //Debug.Log(vec3dir);
        }
        else if (this.transform.position.y + this.GetComponent<BoxCollider2D>().offset.y - this.GetComponent<BoxCollider2D>().size.y / 2 < Camera.main.ScreenToWorldPoint(this.transform.position).y) //�Ʒ��� ȭ�� �Ѿ��
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.up;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

            //Debug.Log(vec3dir);
        }

        else
        {
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }

        if (isAttack == true && isDie == false)
        {
            StartCoroutine(nameof(AttackAnim));
            StartCoroutine(nameof(AttackCoroutine));
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

        //Debug.Log(start + "\n" + end);
        for(int i = 0; i < 5; i++)
        {
            GameObject attack = Instantiate(attackPrefab);
            attack.transform.position = this.transform.position;
            attack.GetComponent<Attack>().SetPower(power);
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