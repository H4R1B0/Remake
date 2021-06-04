using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip : MonoBehaviour
{
    public float speed = 50;

    void Update()
    {
        transform.Translate((Vector3.left+Vector3.down) * Time.deltaTime * speed);

        CheckInScreen();
    }
    private void CheckInScreen()
    {
        if (transform.position.y < -100)
        {            
            Destroy(this.gameObject);
        }
    }
}
