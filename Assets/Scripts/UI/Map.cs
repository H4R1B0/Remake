using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private Vector3 mapOriginPos; //맵 이미지 원래 위치
    private Image mapImage; //맵 사진
    private float delta = 10; //맵이 상하로 움직일 폭
    private float speed = 2.2f; //맵이 상하로 움직이는 속도

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
