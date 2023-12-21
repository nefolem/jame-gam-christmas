using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _glowingStartButton;
    private Animator _animator;
    [SerializeField] private AudioClip _garlandHum;
    private AudioSource _source;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        _source.PlayOneShot(_garlandHum);
        StartCoroutine(EnableGarland());
        

    }



    private void OnMouseExit()
    {
        _animator.enabled = false;
        _source.Stop();
        _glowingStartButton.SetActive(false);
    }

    IEnumerator EnableGarland()
    {
        yield return new WaitForSeconds(1);
        _animator.enabled = true;
        _glowingStartButton.SetActive(true);
    }
}
