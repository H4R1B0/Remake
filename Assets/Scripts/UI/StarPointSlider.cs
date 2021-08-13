using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPointSlider : MonoBehaviour
{
    private Slider starPointSlider; //스타포인트 슬라이더

    void Start()
    {
        starPointSlider = GameObject.Find("StarPointSlider").GetComponent<Slider>();
    }

    void Update()
    {
        starPointSlider.value = Player.instance.StarPoint;

        //플레이어의 스타포인트가 10개 이상 있는경우
        if (Player.instance.StarPoint >= 10)
        {
            //플레이어의 스타포인트레벨이 5보다 작으면 100(+50)만큼 수정 지급 
            if (Player.instance.StarPointLevel < 5)
            {
                Player.instance.Crystal += 100 + (Player.instance.StarPointLevel - 1) * 50;
            }
            else
            {
                Player.instance.Crystal += 300;
            }
            Player.instance.StarPoint -= 10;
        }
    }
}
