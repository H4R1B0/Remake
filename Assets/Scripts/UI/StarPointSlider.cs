using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPointSlider : MonoBehaviour
{
    private Slider starPointSlider; //��Ÿ����Ʈ �����̴�
    // Start is called before the first frame update
    void Start()
    {
        starPointSlider = GameObject.Find("StarPointSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        starPointSlider.value = Player.instance.StarPoint;
    }
}
