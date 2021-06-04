using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public float speed = 100; //이동 속도
    float width = 0; //이미지 가로
    float height = 0; //이미지 세로

    void Start()
    {
        //이미지 크기대로 변경
        width = GetComponent<Image>().preferredWidth;
        height = GetComponent<Image>().preferredHeight;
        speed = 10000000 / (width * height); //이미지 크기에 따라 이동 속도 변경
        transform.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        CheckInScreen(); //화면 밖으로 나갔는지 확인
    }
    private void CheckInScreen()
    {
        if (transform.position.x > Screen.width + width/2)
        {
            //Debug.Log(transform.position.x);
            Destroy(this.gameObject);
        }
    }
}
