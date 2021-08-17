using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class UnitControl : MonoBehaviour
{
    private Player player; //플레이어

    private GameObject selectedObject = null; //터치시 선택된 오브젝트

    private bool pointerDown; //터치했는지
    private float pointerDownTimer; //터치중인 시간
    [SerializeField]
    public float requiredHoldTime; //이동에 요구되는 터치 시간

    public Image image; //unitclone으로 생성할 Image
    private Image unitClone; //선택된 오브젝트 이동시 미리보기 표시할 오브젝트

    private Tilemap UnitBoard; //유닛이 생성되는 타일맵

    private Vector3 adjustVector = new Vector3(0, -0.7f, 0); //마우스 수정용 벡터

    public GameObject UnitDeletePanel; //유닛 삭제하는 패널

    private void Awake()
    {
        UnitBoard = GameObject.Find("UnitBoard").GetComponent<Tilemap>();
    }
    private void Start()
    {
        UnitDeletePanel.SetActive(false);
        player = Player.instance;
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
                    unitClone.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    //마우스 입력 좌표 받기
                    //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    unitClone.transform.position = Input.mousePosition + new Vector3(0, 40, 0);
                    //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    //unitClone.transform.position = Input.mousePosition;
                    UnitDeletePanel.SetActive(true);
                }
            }

            //유닛이 삭제패널위에 있을때 붉게
            if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)UnitDeletePanel.transform, Input.mousePosition))
            {
                UnitDeletePanel.GetComponent<Image>().color = new Color(255 / 255, 98 / 255f, 98 / 255f);
            }
            else
            {
                UnitDeletePanel.GetComponent<Image>().color = new Color(1, 1, 1);
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
                selectedObject = hitInformation.transform.gameObject;
            }
            if (EventSystem.current.IsPointerOverGameObject() == false && hitInformation.transform != null)
            {
                pointerDown = true; //터치중

                //마우스 입력시 이미지 복제
                unitClone = Instantiate(image, GameObject.Find("BattleUI").transform);
                unitClone.sprite = hitInformation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

                //터치시에 생성되는 미리보기 작게
                unitClone.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                float width = unitClone.GetComponent<Image>().preferredWidth;
                float height = unitClone.GetComponent<Image>().preferredHeight;
                unitClone.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                unitClone.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                //Debug.Log("OnPointerDown");
            }
        }

        //터치 땠을때
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("마우스 업");

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //터치한 좌표값 얻기
            Debug.Log(touchPos);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward); //터치한 오브젝트

            //Debug.Log(hitInformation.transform);

            if (pointerDown == true)
            {
                Vector3Int v3Int = UnitBoard.WorldToCell((Vector3)touchPos); //고정
                if (v3Int.x >= 0 && v3Int.x <= 3 && v3Int.y >= 0 && v3Int.y <= 3 && UnitBoard.GetColor(v3Int) != Color.green)
                {
                    UnitBoard.RefreshTile(UnitBoard.WorldToCell(selectedObject.transform.position + adjustVector));
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

                //패널 범위내에 마우스 있을경우 실행
                if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)UnitDeletePanel.transform, Input.mousePosition))
                {
                    Debug.Log("유닛 삭제");
                    UnitBoard.RefreshTile(UnitBoard.WorldToCell(selectedObject.transform.position + adjustVector)); //타일 색 원래대로
                    player.Crystal += selectedObject.GetComponent<Unit>().UnitPrice; //유닛 판매 비용 돌려주기
                    Destroy(selectedObject.gameObject); //선택된 유닛 삭제
                    
                    player.CallUnitCount--; //플레이어 유닛수 감소
                }
                //Debug.Log(player.CallUnitCount);
                UnitDeletePanel.SetActive(false);
            }

        }
    }
}
