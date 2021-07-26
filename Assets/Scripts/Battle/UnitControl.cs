using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class UnitControl : MonoBehaviour
{
    private GameObject selectedObject = null; //터치시 선택된 오브젝트

    private bool pointerDown; //터치했는지
    private float pointerDownTimer; //터치중인 시간
    [SerializeField]
    public float requiredHoldTime; //이동에 요구되는 터치 시간

    private GameObject unitClone; //선택된 오브젝트 이동시 미리보기 표시할 오브젝트

    private Tilemap UnitBoard; //유닛이 생성되는 타일맵

    private Vector3 adjustVector = new Vector3(0, -0.7f, 0); //마우스 수정용 벡터

    private void Awake()
    {
        UnitBoard = GameObject.Find("UnitBoard").GetComponent<Tilemap>();
    }

    void Update()
    {
        //터치중인경우
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= requiredHoldTime)
            {
                if (unitClone != null)
                {
                    //투명도 절반
                    Color color = selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    color.a = 0.5f;
                    selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
                    unitClone.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    //Debug.Log("이동 가능");
                    //UnitSelect.instance.UnitSale();
                    unitClone.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    //Debug.Log("드래그");
                    //마우스 입력 좌표 받기
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    unitClone.transform.position = mousePosition + new Vector3(0, 0.7f, 10);

                }
            }

        }

        //터치 했을때
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("마우스 다운");

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //터치한 좌표값 얻기
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward); //터치한 오브젝트

            //선택된 오브젝트가 유닛인지
            if (hitInformation.transform != null && hitInformation.transform.gameObject.CompareTag("Unit") == true)
            {
                //Debug.Log(hitInformation.transform);
                selectedObject = hitInformation.transform.gameObject;
            }
            if (EventSystem.current.IsPointerOverGameObject() == false && hitInformation.transform != null)
            {
                //GameManagerTest.instance.UnitTouch.GetComponent<Button>().interactable = true;
                pointerDown = true; //터치중
                //마우스 입력시 이미지 복제
                unitClone = Instantiate(hitInformation.transform.GetChild(0).gameObject, transform.position, Quaternion.identity);

                //Debug.Log("이동");
                //터치시에 생성되는 미리보기 작게
                unitClone.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                unitClone.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                //Debug.Log("OnPointerDown");
            }
        }

        //터치 땠을때
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("마우스 업");

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //터치한 좌표값 얻기
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward); //터치한 오브젝트

            if (pointerDown == true)
            {
                Vector3Int v3Int = UnitBoard.WorldToCell(unitClone.transform.position + adjustVector); //고정
                if (v3Int.x >= 0 && v3Int.x <= 3 && v3Int.y >= 0 && v3Int.y <= 3 && UnitBoard.GetColor(v3Int) != Color.green)
                {
                    UnitBoard.RefreshTile(UnitBoard.WorldToCell(selectedObject.transform.position+adjustVector));
                    //Debug.Log(UnitBoard.WorldToCell(selectedObject.transform.position+adjustVector) + "삭제");
                    selectedObject.transform.position = UnitBoard.CellToWorld(v3Int) + new Vector3(0.9f, 1.4f, 0); //객체의 위치 조정
                    UnitBoard.SetTileFlags(v3Int, TileFlags.None);
                    UnitBoard.SetColor(v3Int, Color.green);
                }
                Destroy(unitClone.gameObject);
                Color color = selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                color.a = 1f;
                selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;

                pointerDown = false;
                pointerDownTimer = 0;
                //Debug.Log("OnPointerUp");

            }

        }
    }
}
