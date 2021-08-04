using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballObject : Attack
{
    public GameObject HitFire; //�浹ȿ�� ����Ʈ

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(vec3dir * Time.deltaTime * moveSpeed);
        CheckInScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.tag == "Monster")
        {
            //Debug.Log("���̾ ��ƼŬ");
            collision.gameObject.GetComponent<LivingEntity>().OnDamage(power, false);
            Instantiate(HitFire, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void CheckInScreen()
    {
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        if (targetScreenPos.x > Screen.width || targetScreenPos.x < 0 || targetScreenPos.y > Screen.height || targetScreenPos.y < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
