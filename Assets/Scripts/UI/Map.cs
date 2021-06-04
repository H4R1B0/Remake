using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private Vector3 MapOriginPos; //맵 이미지 원래 위치
    private Image map; //맵 사진
    private float delta = 10; //맵이 상하로 움직일 폭
    private float speed = 2.2f; //맵이 상하로 움직일 속도

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
