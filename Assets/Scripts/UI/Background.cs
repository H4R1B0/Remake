using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public List<Sprite> Clouds; //구름 이미지들
    public Transform[] CloudLocs; //구름 생성 위치

    public Transform AirshipLoc; //비행선 생성 위치

    public GameObject CloudPrefab; //구름 프리팹
    public GameObject AirShipPrefab; //비행선 프리팹

    public Transform MapBack; //맵 뒤에 생성
    public Transform MapFront; //맵 앞에 생성
    private int cloudIndex = 0; //구름 생성 위치 순서
    private GameObject cloud;
    private GameObject airship;

    void Start()
    {
        StartCoroutine(nameof(CloudCoroutine));
        StartCoroutine(nameof(AirShipCoroutine));
    }
    void CreateCloud()
    {
        int randCreate = Random.Range(0, 100);
        //50% 확률로 구름 생성
        if (randCreate >= 0 && randCreate < 50)
        {
            //랜덤한 숫자로 구름 이미지 변경
            int idx = Random.Range(0, Clouds.Count);
            CloudPrefab.GetComponent<Image>().sprite = Clouds[idx];

            int randBF = Random.Range(0, 100);
            if (randBF >= 0 && randBF < 50)
                cloud = Instantiate(CloudPrefab, CloudLocs[(cloudIndex++) % CloudLocs.Length].position, Quaternion.identity, MapBack.transform);
            else
                cloud = Instantiate(CloudPrefab, CloudLocs[(cloudIndex++) % CloudLocs.Length].position, Quaternion.identity, MapFront.transform);
        }
    }
    void CreateAirShip()
    {
        int rand = Random.Range(0, 100);
        //50% 확률로 비행선 생성
        if (rand >= 0 && rand < 50)
        {
            int randBF = Random.Range(0, 100);
            if (randBF >= 0 && randBF < 50)
                airship = Instantiate(AirShipPrefab, AirshipLoc.transform.position, Quaternion.identity, MapBack.transform);
            else
                airship = Instantiate(AirShipPrefab, AirshipLoc.transform.position, Quaternion.identity, MapFront.transform);
        }
    }
    IEnumerator CloudCoroutine()
    {
        while (true)
        {
            //Debug.Log("코루틴");
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
