using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    Rigidbody2D rb2d;

    bool tap = false;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start() {

    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) { //INFO: Input functions must to be called always from Update method
            tap = true;
        }
    }

    void FixedUpdate() {
        if(GameController.instance.gameState == GameState.PLAYING) {
            if(tap) {
                rb2d.AddForce(transform.up * GameController.instance.jumpForce, ForceMode2D.Impulse);
                tap = false;
            }
        }
    }
}
