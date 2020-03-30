using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour {

    bool smallSync, bigSync;

    void Start() {

    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.transform.CompareTag("bound") || collision.gameObject.transform.CompareTag("saw")) { //Si toca un limite o un saw
            PlayerDie();
        } else if(collision.gameObject.transform.CompareTag("bigCircle")) {
            bigSync = true;
            CheckSynchronization(collision.gameObject.transform.parent.gameObject); //Le pasamos el gameObject del padre, el objeto que contiene toda la estructura sync
        } else if(collision.gameObject.transform.CompareTag("smallCircles")) {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            collision.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            smallSync = true;
            CheckSynchronization(collision.gameObject.transform.parent.gameObject); //Le pasamos el gameObject del padre, el objeto que contiene toda la estructura sync
        }
    }

    void CheckSynchronization(GameObject syncStructure) {
        if(smallSync && bigSync) {                                               //All Syncs less EasySync and SawSync
            Destroy(syncStructure);
        } else if(syncStructure.transform.CompareTag("easySync") && smallSync) { //EasySync
            Destroy(syncStructure);
        } else if(syncStructure.transform.CompareTag("sawSync") && smallSync) {  //SawSync
            Destroy(syncStructure);
        }
    }

    void PlayerDie() {
        if(GameController.instance.gameState == GameState.PLAYING) {
            GetComponent<CircleCollider2D>().isTrigger = false;
            GameController.instance.gameState = GameState.GAMEOVER;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.transform.CompareTag("smallCircles")) {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            collision.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            smallSync = false;
        } else if(collision.gameObject.transform.CompareTag("bigCircle")) {
            bigSync = false;
        }
    }


}
