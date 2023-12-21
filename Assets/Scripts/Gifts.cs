using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gifts : MonoBehaviour
{
    [SerializeField] TMP_Text _giftCountText;
    [SerializeField] private List<GameObject> gifts = new();
    [SerializeField] private int _maxGiftsCount = 100;
    public int _currentGiftsCount;
    // Start is called before the first frame update
    void Start()
    {
        _currentGiftsCount = _maxGiftsCount;

        _giftCountText.text = _currentGiftsCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _giftCountText.text = _currentGiftsCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() && _currentGiftsCount != 0)
        {
            Instantiate(gifts[Random.Range(0, gifts.Count)], transform.position, Quaternion.identity);
            _currentGiftsCount--;
        }
    }
}
