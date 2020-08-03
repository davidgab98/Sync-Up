using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mueve los syncs en sus respectivas lineas y los mantiene dentro de los limites horizontales
public class RidingLines : MonoBehaviour {
    Rigidbody2D rb2d;

    float rideSpeed;
    float horizontalLimitLeft, horizontalLimitRight;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start() {
        AssignSpeed();

        //Assign limits from GameController
        horizontalLimitLeft  = GameController.instance.horizontalLimitLeft;
        horizontalLimitRight = GameController.instance.horizontalLimitRight;
    }

    void AssignSpeed() {
        rideSpeed = GameController.instance.rideSyncsSpeed;
        if(Random.Range(0, 2) == 0) {
            rideSpeed *= -1;
        }
    }

    void FixedUpdate() {
        if(GameController.instance.gameState == GameState.PLAYING || GameController.instance.gameState == GameState.GAMEOVER) {
            KeepBetweenBounds();
            Ride();
        }
    }

    void KeepBetweenBounds() {
        if(transform.position.x < horizontalLimitLeft || transform.position.x > horizontalLimitRight) {
            rideSpeed *= -1;
        }
    }

    void Ride() {
        rb2d.MovePosition(rb2d.position + new Vector2(rideSpeed, 0) * Time.fixedDeltaTime);
    }

}
