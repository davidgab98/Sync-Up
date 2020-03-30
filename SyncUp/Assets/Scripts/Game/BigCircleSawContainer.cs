using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCircleSawContainer : MonoBehaviour {
    float rideSpeed1;
    float rideSpeed2;

    float horizontalLimitLeft, horizontalLimitRight;

    Rigidbody2D rb2dChild1;
    Rigidbody2D rb2dChild2;
    Transform transformChild1;
    Transform transformChild2;

    private void Awake() {
        rb2dChild1 = transform.GetChild(0).GetComponent<Rigidbody2D>();
        rb2dChild2 = transform.GetChild(1).GetComponent<Rigidbody2D>();

        transformChild1 = transform.GetChild(0);
        transformChild2 = transform.GetChild(1);
    }

    // Start is called before the first frame update
    void Start() {
        //Assign randomly + and - rideSpeed to BigCircle and Saw 
        if(Random.Range(0, 2) == 0) {
            rideSpeed1 = GameController.instance.rideSyncsSpeed;
            rideSpeed2 = -GameController.instance.rideSyncsSpeed;
        } else {
            rideSpeed1 = -GameController.instance.rideSyncsSpeed;
            rideSpeed2 = GameController.instance.rideSyncsSpeed;
        }

        horizontalLimitLeft = GameController.instance.horizontalLimitLeft;
        horizontalLimitRight = GameController.instance.horizontalLimitRight;
    }

    void FixedUpdate() {
        if(GameController.instance.gameState == GameState.PLAYING || GameController.instance.gameState == GameState.GAMEOVER) {
            KeepChildsBetweenBounds();
            RideChilds();
        }
    }

    void RideChilds() {
        rb2dChild1.MovePosition(rb2dChild1.position + new Vector2(rideSpeed1, 0) * Time.fixedDeltaTime);
        rb2dChild2.MovePosition(rb2dChild2.position + new Vector2(rideSpeed2, 0) * Time.fixedDeltaTime);
    }

    void KeepChildsBetweenBounds() {
        if(transformChild1.position.x <= horizontalLimitLeft || transformChild1.position.x >= horizontalLimitRight) {
            rideSpeed1 *= -1;
        } 
        
        if(transformChild2.position.x <= horizontalLimitLeft || transformChild2.position.x >= horizontalLimitRight) {
            rideSpeed2 *= -1;
        }
    }
}
