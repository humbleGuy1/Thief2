using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmActivator : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _speed;

    private float _minVolume;
    private float _maxVolume;
    private IEnumerator _increasingVolume;
    private IEnumerator _decreasingVolume;

    private void Start()
    {
        _minVolume = 0;
        _maxVolume = 1;
        _increasingVolume = ChangeVolume(_sound, _maxVolume, _speed);
        _decreasingVolume = ChangeVolume(_sound, _minVolume, _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _sound.volume = _minVolume;
            _sound.Play();
            StopCoroutine(_decreasingVolume);
            StartCoroutine(_increasingVolume);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            StopCoroutine(_increasingVolume);
            StartCoroutine(_decreasingVolume);

            if (_sound.volume == _minVolume)
            {
                _sound.Stop();
            }
        }
    }

    private IEnumerator ChangeVolume(AudioSource sound, float _targetVolume, float speed)
    {
        while (true)
        {
            _sound.volume = Mathf.MoveTowards(sound.volume, _targetVolume, speed * Time.deltaTime);
            yield return null;
        }
    }
}
