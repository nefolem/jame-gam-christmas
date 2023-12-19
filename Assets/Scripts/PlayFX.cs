using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFX : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField] private GameObject _damageEffectPrefab;
    [SerializeField] private GameObject _deathEffectPrefab;
    [SerializeField] private GameObject _sprite;

    [Header("Audio Effects")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _stepSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDamageEffect(Vector3 position)
    {
        InstantiateEffect(_damageEffectPrefab, position);
        PlaySound(_damageSound);
    }

    public void PlayDeathEffect(Vector3 position)
    {
        InstantiateEffect(_deathEffectPrefab, position);
        PlaySound(_deathSound);
    }

    public void PlayStepEffect(Vector3 position)
    {
        InstantiateEffect(_sprite, position);
        PlaySound(_stepSound);
    }

    private void InstantiateEffect(GameObject prefab, Vector3 position)
    {
        if (prefab != null)
        {
            var fx = Instantiate(prefab, position, transform.rotation);
            Destroy(fx, 3);
            
        }
    }

    private void PlaySound(AudioClip audioClip)
    {
        _audioSource?.PlayOneShot(audioClip);
    }
}
