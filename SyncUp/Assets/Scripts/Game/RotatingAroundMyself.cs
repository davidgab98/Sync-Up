using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAroundMyself : MonoBehaviour {

    float rotatingSpeed;

    // Start is called before the first frame update
    void Start() {
        rotatingSpeed = GameController.instance.rotatingSpeed;
        if(Random.Range(0, 2) == 0) {
            rotatingSpeed *= -1;
        }
    }

    // Update is called once per frame
    void Update() {
        if(GameController.instance.gameState == GameState.PLAYING || GameController.instance.gameState == GameState.GAMEOVER) {
            Rotate();
        }
    }

    void Rotate() {
        transform.Rotate(0, 0, rotatingSpeed * Time.deltaTime);
    }
}
