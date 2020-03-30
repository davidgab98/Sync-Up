using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour {
    void Start() {

    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.transform.tag == "bound") {
            PlayerOutOfBounds();
        }
    }

    void PlayerOutOfBounds() {
        if(GameController.instance.gameState == GameState.PLAYING) {
            GameController.instance.gameState = GameState.GAMEOVER;
            Destroy(gameObject);
        }
    }
}
