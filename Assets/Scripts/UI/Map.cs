using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private Vector3 MapOriginPos; //�� �̹��� ���� ��ġ
    private Image map; //�� ����
    private float delta = 10; //���� ���Ϸ� ������ ��
    private float speed = 2.2f; //���� ���Ϸ� ������ �ӵ�

    void Start()
    {
        map = GetComponent<Image>();
        MapOriginPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = MapOriginPos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        map.transform.localPosition = v;
    }
}
