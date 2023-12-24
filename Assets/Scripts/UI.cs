using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] List<TMP_Text> _giftCountText;
    [SerializeField] private GameObject _glowingStartButton;
    private Animator _animator;
    [SerializeField] private AudioClip _garlandHum;
    private AudioSource _source;
    public bool _isStarted;

    public static UI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    public void SetGiftsCount(int giftsCount)
    {
        foreach (var text in _giftCountText) 
        {
            if(giftsCount < 100)
            {
            text.text = $"Quota: \r\n<color=#ff0000ff>{giftsCount.ToString("d3")}</color> / 100";

            }
            else
            {
                text.text = $"Quota: \r\n<color=#00ff00ff>{giftsCount.ToString("d3")}</color> / 100";
            }
        }
        
    }

    private void OnMouseEnter()
    {
        _source.PlayOneShot(_garlandHum);
        
        StartCoroutine(EnableGarland());      

    }

    private void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameIntroManager.Instance.StartTimeline();
            _isStarted = true;
        }
    }

    private void OnMouseExit()
    {
        if (!_isStarted)
        {
            StopCoroutine(EnableGarland());
            _animator.enabled = false;
            _source.Stop();
            _glowingStartButton.SetActive(false);
        }
        
    }

    IEnumerator EnableGarland()
    {
        yield return new WaitForSeconds(1);
        _animator.enabled = true;
        _glowingStartButton.SetActive(true);
    }
}
