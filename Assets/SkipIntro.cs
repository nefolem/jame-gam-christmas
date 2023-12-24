using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameIntroManager.Instance.SkipIntro();
            
        }
    }
}
