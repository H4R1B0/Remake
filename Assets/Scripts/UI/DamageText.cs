using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText; //표시할 텍스트
    private int damage; //표시할 데미지
    public int Damage
    {
        set
        {
            damage = value;
        }
    }

    private Color alpha; //투명도
    private float moveSpeed = 100; //위로 올라가는 속도
    private float alphaSpeed = 2; //투명해지는 속도
    private float destroyTime = 1; //1초 뒤 파괴

    void Start()
    {
        transform.SetParent(GameObject.Find("BattleUI").transform); //부모 UI

        damageText = GetComponent<TextMeshProUGUI>();
        alpha = damageText.color;
        damageText.text = damage.ToString();

        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);// 텍스트 위치
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        damageText.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}