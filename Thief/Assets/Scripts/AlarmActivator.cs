using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmActivator : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _pathTime;
    [SerializeField] private float _pathRunningTime;
    private float _startVolume;
    private float _targetVolume;

    private void Start()
    {
        _startVolume = 0;
        _targetVolume = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _pathRunningTime = 0;
            _alarmSound.volume = _startVolume;
            _alarmSound.Play();
            StopAllCoroutines();
            StartCoroutine(IncreaseVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            
            _pathRunningTime = 0;
            StopAllCoroutines();
            StartCoroutine(DecreaseVolume());

            if (_alarmSound.volume == _startVolume)
            {
                _alarmSound.Stop();
            }
            
        }
    }

    private IEnumerator IncreaseVolume()
    {

        while (_alarmSound.volume < _targetVolume)
        {
            _pathRunningTime += Time.deltaTime;
            _alarmSound.volume = Mathf.MoveTowards(_startVolume, _targetVolume, _pathRunningTime / _pathTime);
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        while (_alarmSound.volume > _startVolume)
        {
            _pathRunningTime += Time.deltaTime;
            _alarmSound.volume = Mathf.MoveTowards(_targetVolume, _startVolume, _pathRunningTime / _pathTime);
            yield return null;
        }
    }

}
