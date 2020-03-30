using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//###### MOVE BACKGROUND #######//
// Mueve los fondos 1 y 2       //
//##############################//

public class ScrollingObject : MonoBehaviour {
    public float scrollSpeed = 50;

    private Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start() {
        rb2d.velocity = new Vector2(0, -scrollSpeed);
    }

    void Update() {

    }
}
