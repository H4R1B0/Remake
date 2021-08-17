using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class UnitControl : MonoBehaviour
{
    private Player player; //�÷��̾�

    private GameObject selectedObject = null; //��ġ�� ���õ� ������Ʈ

    private bool pointerDown; //��ġ�ߴ���
    private float pointerDownTimer; //��ġ���� �ð�
    [SerializeField]
    public float requiredHoldTime; //�̵��� �䱸�Ǵ� ��ġ �ð�

    public Image image; //unitclone���� ������ Image
    private Image unitClone; //���õ� ������Ʈ �̵��� �̸����� ǥ���� ������Ʈ

    private Tilemap UnitBoard; //������ �����Ǵ� Ÿ�ϸ�

    private Vector3 adjustVector = new Vector3(0, -0.7f, 0); //���콺 ������ ����

    public GameObject UnitDeletePanel; //���� �����ϴ� �г�

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
        //��ġ���ΰ��
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= requiredHoldTime)
            {
                if (unitClone != null)
                {
                    //���� ����
                    Color color = selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    color.a = 0.5f;
                    selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
                    unitClone.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    //���콺 �Է� ��ǥ �ޱ�
                    //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    unitClone.transform.position = Input.mousePosition + new Vector3(0, 40, 0);
                    //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    //unitClone.transform.position = Input.mousePosition;
                    UnitDeletePanel.SetActive(true);
                }
            }

            //������ �����г����� ������ �Ӱ�
            if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)UnitDeletePanel.transform, Input.mousePosition))
            {
                UnitDeletePanel.GetComponent<Image>().color = new Color(255 / 255, 98 / 255f, 98 / 255f);
            }
            else
            {
                UnitDeletePanel.GetComponent<Image>().color = new Color(1, 1, 1);
            }
        }

        //��ġ ������
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("���콺 �ٿ�");

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //��ġ�� ��ǥ�� ���
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward); //��ġ�� ������Ʈ

            //���õ� ������Ʈ�� ��������
            if (hitInformation.transform != null && hitInformation.transform.gameObject.CompareTag("Unit") == true)
            {
                selectedObject = hitInformation.transform.gameObject;
            }
            if (EventSystem.current.IsPointerOverGameObject() == false && hitInformation.transform != null)
            {
                pointerDown = true; //��ġ��

                //���콺 �Է½� �̹��� ����
                unitClone = Instantiate(image, GameObject.Find("BattleUI").transform);
                unitClone.sprite = hitInformation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

                //��ġ�ÿ� �����Ǵ� �̸����� �۰�
                unitClone.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                float width = unitClone.GetComponent<Image>().preferredWidth;
                float height = unitClone.GetComponent<Image>().preferredHeight;
                unitClone.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                unitClone.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                //Debug.Log("OnPointerDown");
            }
        }

        //��ġ ������
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("���콺 ��");

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //��ġ�� ��ǥ�� ���
            Debug.Log(touchPos);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward); //��ġ�� ������Ʈ

            //Debug.Log(hitInformation.transform);

            if (pointerDown == true)
            {
                Vector3Int v3Int = UnitBoard.WorldToCell((Vector3)touchPos); //����
                if (v3Int.x >= 0 && v3Int.x <= 3 && v3Int.y >= 0 && v3Int.y <= 3 && UnitBoard.GetColor(v3Int) != Color.green)
                {
                    UnitBoard.RefreshTile(UnitBoard.WorldToCell(selectedObject.transform.position + adjustVector));
                    //Debug.Log(UnitBoard.WorldToCell(selectedObject.transform.position+adjustVector) + "����");
                    selectedObject.transform.position = UnitBoard.CellToWorld(v3Int) + new Vector3(0.9f, 1.4f, 0); //��ü�� ��ġ ����
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

                //�г� �������� ���콺 ������� ����
                if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)UnitDeletePanel.transform, Input.mousePosition))
                {
                    Debug.Log("���� ����");
                    UnitBoard.RefreshTile(UnitBoard.WorldToCell(selectedObject.transform.position + adjustVector)); //Ÿ�� �� �������
                    player.Crystal += selectedObject.GetComponent<Unit>().UnitPrice; //���� �Ǹ� ��� �����ֱ�
                    Destroy(selectedObject.gameObject); //���õ� ���� ����
                    
                    player.CallUnitCount--; //�÷��̾� ���ּ� ����
                }
                //Debug.Log(player.CallUnitCount);
                UnitDeletePanel.SetActive(false);
            }

        }
    }
}
