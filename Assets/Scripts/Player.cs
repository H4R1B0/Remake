using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance; //�÷��̾� �ν��Ͻ�
    public List<GameObject> UnitCards; //��ȯ�� ����ī���

    private int coin = 1000000; //����
    private int crystal = 10000; //ũ����Ż

    public int Coin 
    {
        get
        {
            return coin; 
        }
        set 
        {
            coin = value;
        }
    }

    public int Crystal
    {
        get
        {
            return crystal;
        }
        set
        {
            crystal = value;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
