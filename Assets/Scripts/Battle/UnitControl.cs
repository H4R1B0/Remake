using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class UnitControl : MonoBehaviour
{
    private GameObject selectedObject = null; //��ġ�� ���õ� ������Ʈ

    private bool pointerDown; //��ġ�ߴ���
    private float pointerDownTimer; //��ġ���� �ð�
    [SerializeField]
    public float requiredHoldTime; //�̵��� �䱸�Ǵ� ��ġ �ð�

    private GameObject unitClone; //���õ� ������Ʈ �̵��� �̸����� ǥ���� ������Ʈ

    private Tilemap UnitBoard; //������ �����Ǵ� Ÿ�ϸ�

    private Vector3 adjustVector = new Vector3(0, -0.7f, 0); //���콺 ������ ����

    private void Awake()
    {
        UnitBoard = GameObject.Find("UnitBoard").GetComponent<Tilemap>();
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
                    unitClone.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    //Debug.Log("�̵� ����");
                    //UnitSelect.instance.UnitSale();
                    unitClone.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    //Debug.Log("�巡��");
                    //���콺 �Է� ��ǥ �ޱ�
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    unitClone.transform.position = mousePosition + new Vector3(0, 0.7f, 10);

                }
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
                //Debug.Log(hitInformation.transform);
                selectedObject = hitInformation.transform.gameObject;
            }
            if (EventSystem.current.IsPointerOverGameObject() == false && hitInformation.transform != null)
            {
                //GameManagerTest.instance.UnitTouch.GetComponent<Button>().interactable = true;
                pointerDown = true; //��ġ��
                //���콺 �Է½� �̹��� ����
                unitClone = Instantiate(hitInformation.transform.GetChild(0).gameObject, transform.position, Quaternion.identity);

                //Debug.Log("�̵�");
                //��ġ�ÿ� �����Ǵ� �̸����� �۰�
                unitClone.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                unitClone.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                //Debug.Log("OnPointerDown");
            }
        }

        //��ġ ������
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("���콺 ��");

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //��ġ�� ��ǥ�� ���
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward); //��ġ�� ������Ʈ

            if (pointerDown == true)
            {
                Vector3Int v3Int = UnitBoard.WorldToCell(unitClone.transform.position + adjustVector); //����
                if (v3Int.x >= 0 && v3Int.x <= 3 && v3Int.y >= 0 && v3Int.y <= 3 && UnitBoard.GetColor(v3Int) != Color.green)
                {
                    UnitBoard.RefreshTile(UnitBoard.WorldToCell(selectedObject.transform.position+adjustVector));
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

            }

        }
    }
}
