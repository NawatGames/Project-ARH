using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtomBehavior : MonoBehaviour
{
    [SerializeField] private GameObject interactable;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool isInteracting = false;
    private bool isInRange = false;


    public Color inRangeColor;
    public Color outRangeColor;
    public Color interactedColor;
    private Color previewsColor;

    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        outRangeColor = _renderer.color;
        previewsColor = _renderer.color;

    }

    private void FixedUpdate()
    {
        if (isInteracting == true && isInRange == true)
        {
            _renderer.color = interactedColor;
            print("Apertei o botao!");
        }
        else if(isInRange == true && isInteracting == false)
        {
            _renderer.color = previewsColor;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _renderer.color = inRangeColor;
            previewsColor = _renderer.color;
            isInRange = true;
            print("Posso apertar o botao!");
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _renderer.color = outRangeColor;
            previewsColor = _renderer.color;
            isInRange = false;
            print("Nao posso mais apertar o botao!");

        }

    }
}
