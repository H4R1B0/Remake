using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPointSlider : MonoBehaviour
{
    private Slider starPointSlider; //��Ÿ����Ʈ �����̴�

    void Start()
    {
        starPointSlider = GameObject.Find("StarPointSlider").GetComponent<Slider>();
    }

    void Update()
    {
        //�÷��̾��� ��Ÿ����Ʈ�� 10�� �̻� �ִ°��
        if (Player.instance.StarPoint >= 10)
        {
            //�÷��̾��� ��Ÿ����Ʈ ������ ���� 100(+50)��ŭ ���� ����
            Player.instance.Crystal += 100 + (Player.instance.StarPointLevel - 1) * 50;
            Player.instance.StarPoint -= 10;
        }

        starPointSlider.value = Player.instance.StarPoint;
    }
}
