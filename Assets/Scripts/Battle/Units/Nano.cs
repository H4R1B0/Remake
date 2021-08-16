using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nano : LivingEntity
{
    private List<GameObject> FoundTargets; //ã�� Ÿ�ٵ�
    private float shortDis; //Ÿ�ٵ� �߿� ���� ª�� �Ÿ�

    public Slider HPSliderPrefab; //ü�� ������ ������
    public Slider MPSliderPrefab; //���� ������ ������
    private Slider HPSlider; //ü�� ������
    private Slider MPSlider; //���� ������

    //public bool isWeapon = true; //���Ⱑ �ִ���
    //public bool isWeaponRotate = true; //���Ⱑ ȸ���ϴ���
    //[ShowIf("isWeapon")] //���� �������� ǥ��
    //public float attackAnimTime = 0; //���� �ִϸ��̼� ��Ÿ��
    public GameObject attackPrefab; //���� ������

    public GameObject NanoEffectPrefab; //��ų ���� ����Ʈ ������
    private GameObject nanoEffect; //��ų ���� ����Ʈ ������

    private void Start()
    {
        //level = 1; //���� ����

        //������ ���� ���ݷ°� ü�� ����
        originPower = 30; //���� ���ݷ�
        power = originPower; //���ݷ�
        originHealth = 300; //���� ü��
        health = originHealth; //ü��
        maxHealth = health;
        mana = 0;
        //originCritical = critical;

        attackRange = 4f; //���� ����
        attackSpeed = 0.6f; //���� �ӵ�

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

        //���� ����
        if (GameManager.instance.IsStart == true)
        {
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
        }
        //���� ���� �� �̰ų� ���� ���� 
        else
        {
            health = maxHealth; //�ִ� ü������ ȸ��
            mana = 0; //���� �ʱ�ȭ

            animators[0].SetBool("isAttack", false);
        }
    }
    public void OnDestroy()
    {
        Destroy(HPSlider.gameObject);
        Destroy(MPSlider.gameObject);
        Destroy(this.gameObject);
    }

    private void Skill()
    {
        Debug.Log("���� ��ų ����");
        StartCoroutine(nameof(NanoSkill)); //���� ��ų ����
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
    public override void OnDamage(int damage, bool isCritical)
    {
        base.OnDamage(damage, isCritical);

        //ü���� 0���� ������� ��Ȱ��ȭ
        if (health <= 0)
        {
            StopAllCoroutines();
            isAttack = true;
            health = maxHealth;
            mana = 0;
            renderer.material = defaultMaterial;
            GameObject disabledObjects = GameObject.Find("DisabledObjects"); //��Ȱ��ȭ �����ϴ� ������Ʈ
            transform.SetParent(disabledObjects.transform);
            HPSlider.transform.SetParent(disabledObjects.transform);
            MPSlider.transform.SetParent(disabledObjects.transform);
            HPSlider.gameObject.SetActive(false);
            MPSlider.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    //���� �ڷ�ƾ
    IEnumerator AttackAnim()
    {
        animators[0].SetBool("isAttack", true);
        vec3dir = target.transform.position - transform.position;
        vec3dir.Normalize();
        //yield return new WaitForSeconds(animators[0].GetFloat("attackTime")); //���� ��Ÿ��
        yield return null;

        GameObject attack = Instantiate(attackPrefab);
        attack.transform.position = this.transform.position + new Vector3(0, -0.8f, 0);
        attack.GetComponent<Attack>().SetPowerDir(power, target);

        mana += 10; //���ݽ� ���� 10ȹ��
        //target.GetComponent<LivingEntity>().OnDamage(power, false); //����
        animators[0].SetBool("isAttack", false);
    }

    //���� ��Ÿ�� �ڷ�ƾ
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }
    //���� ��ų: 3(+1)�ʰ� �� �ʸ��� �ֺ��Ʊ��� ü���� 100�� ȸ��
    IEnumerator NanoSkill()
    {
        //��������Ʈ
        nanoEffect = Instantiate(NanoEffectPrefab);
        nanoEffect.transform.position = this.transform.position;        
        nanoEffect.GetComponent<ParticleSystem>().Stop(); //����߿��� �ð� ���� ����
        var main = nanoEffect.GetComponent<ParticleSystem>().main;
        main.duration = level + 2;
        nanoEffect.GetComponent<ParticleSystem>().Play();

        GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject foundunit in foundUnits)
        {
            StartCoroutine(foundunit.GetComponent<LivingEntity>().IncreasingHPCoroutine(level + 2, 100));
        }
        yield return null;
    }
}