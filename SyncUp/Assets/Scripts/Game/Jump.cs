using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start() {

    }

    void FixedUpdate() {
        if(GameController.instance.gameState == GameState.PLAYING) {
            if(Input.GetMouseButtonDown(0)) {
                rb2d.AddForce(transform.up * GameController.instance.jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
