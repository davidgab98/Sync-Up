using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingLines : MonoBehaviour {
    Rigidbody2D rb2d;

    float rideSpeed;
    float horizontalLimitLeft, horizontalLimitRight;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start() {
        rideSpeed = GameController.instance.rideSyncsSpeed;
        if(Random.Range(0, 2) == 0) {
            rideSpeed *= -1;
        }

        horizontalLimitLeft = GameController.instance.horizontalLimitLeft;
        horizontalLimitRight = GameController.instance.horizontalLimitRight;
    }

    void FixedUpdate() {
        if(GameController.instance.gameState == GameState.PLAYING || GameController.instance.gameState == GameState.GAMEOVER) {
            KeepBetweenBounds();
            Ride();
        }
    }

    void Ride() {
        rb2d.MovePosition(rb2d.position + new Vector2(rideSpeed, 0) * Time.fixedDeltaTime);
    }

    void KeepBetweenBounds() {
        if(transform.position.x <= horizontalLimitLeft || transform.position.x >= horizontalLimitRight) {
            rideSpeed *= -1;
        }
    }
}
