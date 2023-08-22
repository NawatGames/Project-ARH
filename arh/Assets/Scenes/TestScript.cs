using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // _rb.AddForce(new Vector2(5, 0), ForceMode2D.Force);
        _rb.velocity = new Vector2(5, _rb.velocity.y);
    }
}
