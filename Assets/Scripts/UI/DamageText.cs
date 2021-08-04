using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText; //ǥ���� �ؽ�Ʈ
    private int damage; //ǥ���� ������
    public int Damage
    {
        set
        {
            damage = value;
        }
    }

    private Color alpha; //����
    private float moveSpeed = 100; //���� �ö󰡴� �ӵ�
    private float alphaSpeed = 2; //���������� �ӵ�
    private float destroyTime = 1; //1�� �� �ı�

    void Start()
    {
        transform.SetParent(GameObject.Find("BattleUI").transform); //�θ� UI

        damageText = GetComponent<TextMeshProUGUI>();
        alpha = damageText.color;
        damageText.text = damage.ToString();

        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);// �ؽ�Ʈ ��ġ
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // �ؽ�Ʈ ���İ�
        damageText.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}