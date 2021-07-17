using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFireWeapon : Attack
{
    private Vector3 startPos; //시작하는 위치
    private float preTime; //초기 시간
    private float duration = 0.7f; //속도(낮을수록 빨라짐)

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
        //포물선 이동
        Parabolic(startPos, target.transform.position, (Time.time - preTime) / duration);

        //총알이 화면 밖으로 나갈경우 파괴
        if (this.transform.position.x < Camera.main.ScreenToWorldPoint(this.transform.position).x)
            Destroy(this.gameObject);
        //Debug.Log(Camera.main.ScreenToWorldPoint(this.transform.position));
    }
}