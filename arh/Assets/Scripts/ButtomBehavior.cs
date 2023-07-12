using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtomBehavior : MonoBehaviour
{
    [SerializeField] private GameObject interactable;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool isInteracting;
    private bool isInRange;


    public Color inRangeColor;
    public Color outRangeColor;
    public Color interactedColor;

    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        outRangeColor = _renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange == true && isInteracting == true)
        {
            
            
            
            
            
            
            
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _renderer.color = inRangeColor;
            isInRange = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _renderer.color = outRangeColor;
        }

    }
}
