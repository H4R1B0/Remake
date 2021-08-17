using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDanceFireWeapon : Attack
{
    private Vector3 startPos; //�����ϴ� ��ġ
    private float preTime; //�ʱ� �ð�
    private float duration = 0.65f; //�ӵ�(�������� ������)

    private void Awake()
    {
        this.transform.position = new Vector3(this.transform.position.x + 0.14f, this.transform.position.y + 0.7f, this.transform.position.z);
    }
    private void Start()
    {
        startPos = this.transform.position;
        preTime = Time.time;
    }
    void Update()
    {
        //������ �̵�
        Parabolic(startPos, target.transform.position, (Time.time - preTime) / duration);

        //�Ѿ��� ȭ�� ������ ������� �ı�
        CheckInScreen();
    }
}