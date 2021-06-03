using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public float speed=100; //구름 이동속도
    private float width; //가로 크기
    private float height; //세로 크기

    void Start()
    {
        //이미지 크기대로 변경
        width = GetComponent<Image>().preferredWidth;
        height = GetComponent<Image>().preferredHeight;
        speed = 10000000 / (width * height);
        transform.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed); //이동
        
        CheckInScreen(); //화면안에 구름 있는지 확인
    }    
    
    private void CheckInScreen()
    {
        if (transform.position.x > Screen.width+ width/2)
        {
            //Debug.Log(transform.position.x);
            Destroy(this.gameObject);
        }
    }
}
