using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private List<GameObject> enemyList = new();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null || collision.gameObject.CompareTag("Gift"))
        {
            enemyList.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

        }
        //else if (collision.gameObject.GetComponent<Snowball>())
        {

        }
    }
}
