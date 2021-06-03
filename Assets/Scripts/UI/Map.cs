using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private Vector3 mapOriginPos; //�� �̹��� ���� ��ġ
    private Image mapImage; //�� ����
    private float delta = 10; //���� ���Ϸ� ������ ��
    private float speed = 2.2f; //���� ���Ϸ� �����̴� �ӵ�

    // Start is called before the first frame update
    void Start()
    {
        mapImage = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = mapOriginPos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        this.transform.localPosition = v;
    }
}
