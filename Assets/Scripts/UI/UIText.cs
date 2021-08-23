using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : MonoBehaviour
{
    private TextMeshProUGUI uiText; //ǥ���� �ؽ�Ʈ
    private string content; //ǥ���� ����
    public string Content
    {
        set
        {
            content = value;
        }
    }

    private Color alpha; //����
    private float moveSpeed = 100; //���� �ö󰡴� �ӵ�
    private float alphaSpeed = 2; //���������� �ӵ�
    private float destroyTime = 1; //1�� �� �ı�

    private void Awake()
    {
        transform.SetParent(GameObject.Find("BattleUI").transform); //�θ� UI
        uiText = GetComponent<TextMeshProUGUI>();        
    }
    private void Start()
    {
        alpha = uiText.color;
        uiText.text = content;

        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);// �ؽ�Ʈ ��ġ
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // �ؽ�Ʈ ���İ�
        uiText.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}