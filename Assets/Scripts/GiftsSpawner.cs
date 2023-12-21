using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftsSpawner : MonoBehaviour
{
    [SerializeField] TMP_Text _giftCountText;
    [SerializeField] private List<GameObject> gifts = new();
    [SerializeField] private int _maxGiftsCount = 100;
    public int CurrentGiftsCount { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        CurrentGiftsCount = _maxGiftsCount;

        _giftCountText.text = CurrentGiftsCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CurrentGiftsCount);
        _giftCountText.text = CurrentGiftsCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() && CurrentGiftsCount != 0)
        {
            Instantiate(gifts[Random.Range(0, gifts.Count)], transform.position, Quaternion.identity);
            CurrentGiftsCount--;
        }
    }
}
