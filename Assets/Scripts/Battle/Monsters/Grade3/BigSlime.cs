using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigSlime : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    private int baseHP = 400; //�⺻ ü��
    private int roundHP = 50; //����� �߰��Ǵ� ü��
    private int basePower = 30; //�⺻ ���ݷ�
    private int roundPower = 4; //����� �߰��Ǵ� ���ݷ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    private Slider HPSlider; //ü�� ������

    public GameObject Slime; //������ ��ȯ�� ������

    void Start()
    {
        isDie = false;

        moveSpeed *= 1.5f; //�̵��ӵ� ������

        defaultMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material; //�̹��� ���׸��� ����
        renderer = GetComponentInChildren<SpriteRenderer>();

        //������ ���� ���ݷ°� ü�� ����
        power = basePower + roundPower * (GameManager.instance.Round - 1); //���ݷ�
        health = baseHP + roundHP * (GameManager.instance.Round - 1); //ü��
        maxHealth = health;
        //originCritical = critical;

        //attackRange = 5f; //���� ����
        //attackSpeed = 0.5f; //���� �ӵ�
        vec3dir = Vector3.left; //�⺻������ ��������

        animators = GetComponentsInChildren<Animator>(); //�ִϸ����͵� ��������

        //HP, MP ����
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        //animators[0].SetBool("isDie", true);

        rigid = GetComponent<Rigidbody2D>();

        //isAttack = true;
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

        //�¿� �̵�
        if (this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x+this.GetComponent<BoxCollider2D>().size.x/2) //���� ȭ�� �Ѿ��
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.right;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);

            //Debug.Log(vec3dir);
        }
        else if (this.transform.position.x > -Camera.main.ScreenToWorldPoint(this.transform.position).x - this.GetComponent<BoxCollider2D>().size.x / 2) //������ ȭ�� �Ѿ��
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            vec3dir = Vector3.left;
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
        }
    }
    public void OnDestroy()
    {
        Destroy(HPSlider.gameObject);
    }

    //�׾����� �ڷ�ƾ
    IEnumerator DestroyCoroutine()
    {
        //�÷��� �ڷ�ƾ ���߰� ���� ���׸���� ����
        StopCoroutine(nameof(FlashCoroutine));
        renderer.material = defaultMaterial;
        //Debug.Log("FlashCoroutine ����");

        //������ ������ 8���� ��ȯ
        GameObject[] slimes = new GameObject[8];
        for(int i = 0; i < 8; i++)
        {
            slimes[i] = Instantiate(Slime);
            slimes[i].transform.position = this.transform.position - new Vector3(0.3f*i,0,0);
        }

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