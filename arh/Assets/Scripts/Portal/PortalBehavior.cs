using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening.Core;
using UnityEngine.SceneManagement;

public class PortalBehavior : MonoBehaviour
{
    [SerializeField] private int _stoneCollectedCounter = 0;
    [SerializeField] private int _stoneCollectedNeeded;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private bool _isActive = false;

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_isActive == false && col.tag == "Player" && _stoneCollectedCounter == _stoneCollectedNeeded)
        {
            Debug.Log("Portal Ligou!");
            _spriteRenderer.color = Color.green;
            _isActive = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    public void StoneCollected()
    {
        _stoneCollectedCounter++;
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("PlayerMovement_Murata");
    }
}