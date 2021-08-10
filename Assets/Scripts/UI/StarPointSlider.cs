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
            Player.instance.Crystal += 100; //크리스탈 100증가
            Player.instance.StarPoint -= 10;
        }
    }
}
