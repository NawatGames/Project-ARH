using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class StoneBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _targetGameObject;
    [SerializeField] private Vector2 _targetPosition;

    [SerializeField] private float _finalHeight;
    [SerializeField] private float _duration;
    [SerializeField] private bool _wasCollected;
    private float _initialHeight;
    
    public AnimationCurve animationCurve;

    private Sequence _sequence;

    public UnityEvent stoneCollected;

    private void Awake()
    {
        _initialHeight = transform.localPosition.y;

        var ida = transform.DOMoveY(_finalHeight, _duration, false).SetEase(animationCurve);
        var volta = transform.DOMoveY(_initialHeight, _duration, false).SetEase(animationCurve);

        _sequence = DOTween.Sequence().Append((ida)).Append(volta).SetLoops(-1);
    }

    private void FixedUpdate()
    {
        _targetPosition = _targetGameObject.transform.position;

        if (_wasCollected == true)
        {
            _sequence.Kill();
            transform.DOMove(_targetPosition, _duration, false);
            StartCoroutine(DisableCollectableDelay());
            
        }
    }

    private void OnEnable()
    {
        _sequence.Restart();
        _sequence.Play();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && _wasCollected == false)
        {
            OnCollected();
        }
    }

    public void OnCollected()
    {
        Debug.Log("seguindo player!");
        _wasCollected = true;
    }

    IEnumerator DisableCollectableDelay()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        stoneCollected.Invoke();
    }
}
