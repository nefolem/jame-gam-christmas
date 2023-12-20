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
        else if (collision.gameObject.GetComponent<Snowball>())
        {
            Debug.Log("snowball");
            foreach(GameObject go in enemyList)
            {
                go.SetActive(true);
                go.transform.position = transform.position + new Vector3(15f, 10f, 15f);
            }
        }
    }
}
