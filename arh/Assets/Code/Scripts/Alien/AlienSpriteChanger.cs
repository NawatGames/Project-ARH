using UnityEngine;

public class AlienSpriteChanger : MonoBehaviour
{
    private AlienShrinkingScript _alienShrinker;

    void Start()
    {
        _alienShrinker = gameObject.GetComponent<AlienShrinkingScript>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)) //&& alienShrinker.isShrunk == false)
        {
            gameObject.GetComponent<SpriteRenderer>().size = new Vector2 (1f, 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.X)) //&& alienShrinker.isShrunk == true)
        {
            gameObject.GetComponent<SpriteRenderer>().size = new Vector2 (1f, 1f);
        }
    }
}
