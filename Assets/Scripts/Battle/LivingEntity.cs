using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour
{    
    protected int power = 30; //공격력
    protected int originPower; //원래 공격력
    protected int health = 50; //체력
    protected int originHealth; //원래 체력
    protected int maxHealth; //최대 체력
    
    protected float attackRange = 1f; //공격 범위
    protected float attackSpeed = 0f; //공격 속도
    protected int criticalRate = 0; //치명타율
    protected int originCriticalRate = 10; //원래 치명타율
    protected int CriticalDamageRate = 0; //치명타 피해율
    protected int originCriticalDamageRate = 130; //원래 치명타 피해율
    protected Rigidbody2D rigid; //물리
    public GameObject UIText; //데미지, 힐 텍스트
    
    public GameObject PoisonEffect; //맹독 이펙트
    
    public GameObject SternEffect; //스턴 이펙트
    
    protected float moveSpeed = 1.3f; // 이동 속도
    protected Vector3 vec3dir = Vector3.right; //움직이는 방향
    protected GameObject target = null; //타겟으로 되는 적
    protected bool isAttack; //공격 가능한지
    protected bool isStern; //스턴 상태
    protected Animator[] animators; //애니메이터

    public Material FlashWhite; //피격시 변경할 메테리얼
    protected Material defaultMaterial; //기본 메테리얼
    protected Coroutine runningFlashCoroutine = null; //실행중인 코루틴
    protected SpriteRenderer spriteRenderer; //이미지 렌더러

    protected bool isDie; //죽었는지
    public bool IsDie
    {
        get
        {
            return isDie;
        }
    }

    protected bool isAlter = false; //분신인지 확인
    public bool IsAlter
    {
        get
        {
            return isAlter;
        }
    }

    protected List<GameObject> FoundTargets; //찾은 타겟들
    protected float shortDis; //타겟들 중에 가장 짧은 거리
    public Slider HPSliderPrefab; //체력 게이지 프리팹
    protected Slider HPSlider; //체력 게이지

    protected int avoidRate = 0; //회피율

    //OnDamage 메서드
    public virtual void OnDamage(int damage, bool isCritical)
    {
        GameObject DamageText = Instantiate(UIText, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1, 0)), Quaternion.identity);
        //회피할 경우
        int rand = Random.Range(0, 100);
        if (rand >= 0 && rand < avoidRate)
        {
            DamageText.GetComponent<UIText>().Content = "회피";
        }
        else
        {
            health -= damage;
            DamageText.GetComponent<UIText>().Content = damage.ToString();

            //크리티컬인 경우
            if(isCritical == true)
            {
                //Debug.Log("크리티컬");
                DamageText.transform.localScale += new Vector3(0.4f, 0.4f, 0);
            }
        }

        if (runningFlashCoroutine == null)
        {
            runningFlashCoroutine = StartCoroutine(nameof(FlashCoroutine));
        }
        else
        {
            runningFlashCoroutine = StartCoroutine(nameof(FlashCoroutine));
        }
    }

    public void Knockback(Vector2 pos)
    {
        int reaction = transform.position.x - pos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(reaction, 1) * 10, ForceMode2D.Impulse); //넉백 정도
    }

    //출혈 코루틴 : time초간 매 초 damage 피해
    public IEnumerator BleedingCoroutine(int time, int damage, string attribute = "")
    {
        //맹독 이펙트
        if (attribute == "poison")
        {
            GameObject poisonEffect = Instantiate(PoisonEffect, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            poisonEffect.GetComponent<ParticleSystem>().Stop(); //재생중에는 시간 변경 못함
            var main = poisonEffect.GetComponent<ParticleSystem>().main;
            main.duration = time;
            poisonEffect.GetComponent<ParticleSystem>().Play();
        }
        for (int i = 0; i < time; i++)
        {
            OnDamage(damage, false);
            Debug.Log(damage + " 피해");
            yield return new WaitForSeconds(1); //1초 쿨
        }
    }

    //time초간 스턴 코루틴
    public IEnumerator SternCoroutine(int time)
    {
        Debug.Log(time + " 초 간 스턴");
        isStern = true;

        //스턴
        GameObject stern = Instantiate(SternEffect, this.transform.position + new Vector3(0, 0.15f, 0), Quaternion.identity);
        stern.GetComponent<Stern>().DestroySec = time;
        animators[0].speed = 0; //멈춤

        yield return new WaitForSeconds(time); //time초 쿨

        animators[0].speed = 1; //다시 재생
        isStern = false;

    }

    //일정 시간동안 공격력 증가 코루틴
    public IEnumerator IncreasingPowerCoroutine(int powercnt, int time) //증가하는 공격력량, 증가하는 시간
    {
        int origin = power;
        power += powercnt;
        yield return new WaitForSeconds(time);
        power = origin;
    }
    
    //피격시 메테리얼 변경 메서드
    protected IEnumerator FlashCoroutine()
    {
        //현재 메테리얼 변경
        spriteRenderer.material = FlashWhite;
        //0.15초간 대기
        yield return new WaitForSeconds(0.15f);
        //원래 메테리얼 변경
        spriteRenderer.material = defaultMaterial;
        runningFlashCoroutine = null;
    }
}
