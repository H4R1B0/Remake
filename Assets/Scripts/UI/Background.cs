using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public List<Sprite> Clouds; //구름 여러개 이미지
    public Transform[] CloudLoc; //구름 생성 위치
    public Transform AirshipLoc; //비행선 생성 위치

    public GameObject CloudPrefab; //구름 프리팹
    public GameObject AirShipPrefab; //비행선 프리팹

    public GameObject MapBack; //맵 뒤에 생성
    public GameObject MapFront; //맵 앞에 생성

    private int CloudIndex = 0; //구름 생성 위치 순서
    private GameObject Cloud;
    private GameObject Airship;

    void Start()
    {
        StartCoroutine(nameof(CloudCoroutine)); //구름 코루틴
        StartCoroutine(nameof(AirShipCoroutine)); //비행선 코루틴
    }
    void CreateCloud()
    {
        int randCreate = Random.Range(0, 100);
        //50% 확률로 구름 생성
        if (randCreate >= 0 && randCreate < 50)
        {
            //랜덤한 숫자로 구름 이미지 변경
            int randIdx = Random.Range(0, Clouds.Count);
            CloudPrefab.GetComponent<Image>().sprite = Clouds[randIdx];

            //50:50으로 맵 앞뒤에 생성
            int randBF = Random.Range(0, 100);
            if (randBF >= 0 && randBF < 50)
            {
                Cloud = Instantiate(CloudPrefab, CloudLoc[(CloudIndex++) % CloudLoc.Length].position, Quaternion.identity, MapBack.transform);
            }
            else
            {
                Cloud = Instantiate(CloudPrefab, CloudLoc[(CloudIndex++) % CloudLoc.Length].position, Quaternion.identity, MapFront.transform);
            }
        }
    }
    void CreateAirShip()
    {
        int randCreate = Random.Range(0, 100);
        //50% 확률로 비행선 생성
        if (randCreate >= 0 && randCreate < 50)
        {            
            //50:50으로 맵 앞뒤에 생성
            int randBF = Random.Range(0, 100);
            if (randBF >= 0 && randBF < 50)
            {
                Airship = Instantiate(AirShipPrefab, AirshipLoc.position, Quaternion.identity, MapBack.transform);
            }
            else
            {
                Airship = Instantiate(AirShipPrefab, AirshipLoc.position, Quaternion.identity, MapFront.transform);
            }
        }
    }
    IEnumerator CloudCoroutine()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Cloud").Length < 8)
            {
                CreateCloud();
            }
            //구름 스폰 간격 3초
            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator AirShipCoroutine()
    {
        while (true)
        {
            CreateAirShip();
            //비행선 스폰 간격 15초
            yield return new WaitForSeconds(15f);
        }
    }
}
