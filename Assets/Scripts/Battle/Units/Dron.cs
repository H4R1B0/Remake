using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dron : MonoBehaviour
{
    private int power; //공격력
    private int health; //체력
    private int maxHealth; //최대 체력

    private float attackRange; //공격 범위
    private float attackSpeed; //공격 속도
        
    private Rigidbody2D rigid; //물리

    //public GameObject DamageText;
    //public bool isStern; //스턴인지
    private float moveSpeed = 1.3f; // 이동 속도
    private Vector3 vec3dir = Vector3.right; //움직이는 방향
    private GameObject target = null; //타겟으로 되는 적
    private bool isAttack; //공격 가능한지
    private Animator animator; //애니메이터

    public Material FlashWhite; //피격시 변경할 메테리얼
    private Material defaultMaterial; //기본 메테리얼
    private Coroutine runningCoroutine = null; //실행중인 코루틴
    private new Renderer renderer; //이미지 렌더러

    private List<GameObject> FoundTargets; //찾은 타겟들
    private float shortDis; //타겟들 중에 가장 짧은 거리

    public Slider HPSliderPrefab; //체력 게이지 프리팹
    private Slider HPSlider; //체력 게이지

    public GameObject attackPrefab; //공격 프리팹
    private void Awake()
    {
        //생성시 원래 공격력과 체력 저장        
        power = 0; //공격력        
        health = 0; //체력
        maxHealth = health;

        attackRange = 3f; //공격 범위
        attackSpeed = 0.7f; //공격 속도

        animator = GetComponent<Animator>(); //애니메이터 가져오기

        //HP, MP 생성
        HPSlider = Instantiate(HPSliderPrefab, Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position), Quaternion.identity);
        HPSlider.transform.SetParent(GameObject.Find("UnitUIManager").transform);
        HPSlider.maxValue = maxHealth;
        HPSlider.value = health;
        
        isAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        //체력 값, 위치 변경
        HPSlider.value = health;
        HPSlider.maxValue = maxHealth;

        //HP
        HPSlider.transform.Find("HPCount").GetComponent<Text>().text = HPSlider.value.ToString();
        HPSlider.transform.Find("AttackCount").GetComponent<Text>().text = "공격력 : " + power.ToString();
        HPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.Find("HPPosition").position);

        //타겟 향하는
        if (vec3dir.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * 1, transform.localScale.y, transform.localScale.z);
        }

        //타겟이 정해지지 않았거나 죽었을경우 FindMonster
        if (target == null || target.gameObject.activeSelf == false)
        {
            //Debug.Log("타겟 찾기");
            if (target != null && target.gameObject.activeSelf == false)
            {
                //Beast일경우 적이 죽은 경우 체력 회복
                Debug.Log("타겟 죽음");
            }
            FindMonster();
        }
        //타겟이 공격 범위 안에 있을 경우
        if (MonsterInCircle() == true)
        {
            animator.SetBool("isMove", false);
            //공격
            if (isAttack == true)
            {
                StartCoroutine(nameof(AttackAnim));
                StartCoroutine(nameof(AttackCoroutine));
            }
        }
        //타겟쪽으로 이동
        else if (target != null && FoundTargets.Count != 0)
        {
            animator.SetBool("isMove", true);
            transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        }
        //맵에 몬스터가 없을경우
        else if (FoundTargets.Count == 0)
        {
            //isSkill = false; //스킬 초기화
            //power -= level * 10; //공격력 원래대로
            animator.SetBool("isAttack", false);
        }
    }

    //몬스터 찾기
    public void FindMonster()
    {
        //Debug.Log("찾기");
        FoundTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        if (FoundTargets.Count != 0)
        {
            //짧은 거리 찾기
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

    //일정한 범위 내에 몬스터 있는지 확인
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

    //공격 코루틴
    IEnumerator AttackAnim()
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.1f); //공격 쿨타임
        // 원거리        
        GameObject attack = Instantiate(attackPrefab);
        attack.GetComponent<Attack>().SetPowerDir(power, target);
        animator.SetBool("isAttack", false);
    }

    //공격 쿨타임 코루틴
    IEnumerator AttackCoroutine()
    {
        isAttack = false;
        yield return new WaitForSeconds(1f / attackSpeed);
        isAttack = true;
    }

    //드론 파괴 코루틴
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(10);
        Destroy(HPSlider.gameObject);
        Destroy(this.gameObject);
    }
}
