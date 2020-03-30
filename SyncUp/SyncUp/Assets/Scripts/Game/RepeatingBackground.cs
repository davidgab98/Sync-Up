using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {
    float backgroundPortraitLength = 11.9f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(transform.position.y < -backgroundPortraitLength) {
            RepositionBackground();
        }
    }

    void RepositionBackground() {
        transform.Translate(Vector2.up * backgroundPortraitLength * 2);
    }
}
