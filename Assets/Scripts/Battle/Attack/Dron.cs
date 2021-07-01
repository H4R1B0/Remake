using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dron : MonoBehaviour
{
    private int power; //���ݷ�
    private int health; //ü��
    private int maxHealth; //�ִ� ü��

    private float attackRange; //���� ����
    private float attackSpeed; //���� �ӵ�
        
    private Rigidbody2D rigid; //����

    //public GameObject DamageText;
    //public bool isStern; //��������
    private float moveSpeed = 1.3f; // �̵� �ӵ�
    private Vector3 vec3dir = Vector3.right; //�����̴� ����
    private GameObject target = null; //Ÿ������ �Ǵ� ��
    private bool isAttack; //���� ��������
    private Animator animator; //�ִϸ�����

    public Material FlashWhite; //�ǰݽ� ������ ���׸���
    private Material defaultMaterial; //�⺻ ���׸���
    private Coroutine runningCoroutine = null; //�������� �ڷ�ƾ
    private new Renderer renderer; //�̹��� ������

    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    private Slider HPSlider; //ü�� ������

    public GameObject attackPrefab; //���� ������
    private void Awake()
    {
        //������ ���� ���ݷ°� ü�� ����        
        power = 0; //���ݷ�        
        health = 0; //ü��
        maxHealth = health;

        attackRange = 3f; //���� ����
        attackSpeed = 0.7f; //���� �ӵ�

        animator = GetComponent<Animator>(); //�ִϸ����� ��������

        //HP, MP ����
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        
        isAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ü�� ��, ��ġ ����
        HPSlider.value = health;
        HPSlider.maxValue = maxHealth;

        //HP
        HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
        HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "���ݷ� : " + power.ToString();
        HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);

        //Ÿ�� ���ϴ�
        if (vec3dir.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * 1, transform.localScale.y, transform.localScale.z);
        }

        //Ÿ���� �������� �ʾҰų� �׾������ FindMonster
        if (target == null || target.gameObject.activeSelf == false)
        {
            //Debug.Log("Ÿ�� ã��");
            if (target != null && target.gameObject.activeSelf == false)
            {
                //Beast�ϰ�� ���� ���� ��� ü�� ȸ��
                Debug.Log("Ÿ�� ����");
            }
            FindMonster();
        }
        //Ÿ���� ���� ���� �ȿ� ���� ���
        if (MonsterInCircle() == true)
        {
            animator.SetBool("isMove", false);
            //����
            if (isAttack == true)
            {
                StartCoroutine(nameof(AttackAnim));
                StartCoroutine(nameof(AttackCoroutine));
            }
        }
        //Ÿ�������� �̵�
        else if (target != null && FoundTargets.Count != 0)
        {
            animator.SetBool("isMove", true);
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        //�ʿ� ���Ͱ� �������
        else if (FoundTargets.Count == 0)
        {
            //isSkill = false; //��ų �ʱ�ȭ
            //power -= level * 10; //���ݷ� �������
            animator.SetBool("isAttack", false);
        }
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

    public void SetDron(int p, int h)
    {
        power = p;
        health = h;
        maxHealth = health;
        StartCoroutine(nameof(DestroyCoroutine));
    }

    //���� �ڷ�ƾ
    IEnumerator AttackAnim()
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.1f); //���� ��Ÿ��
        // ���Ÿ�        
        GameObject attack = Instantiate(attackPrefab);
        attack.GetComponent<Attack>().SetPowerDir(power, target);
        animator.SetBool("isAttack", false);
    }

    //���� ��Ÿ�� �ڷ�ƾ
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }

    //��� �ı� �ڷ�ƾ
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(10);
        Destroy(HPSlider.gameObject);
        Destroy(this.gameObject);
    }
}
