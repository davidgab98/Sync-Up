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
            CheckSynchronization(collision.gameObject.transform.root.gameObject); //Le pasamos el gameObject del padre, el objeto que contiene toda la estructura sync
        } else if(collision.gameObject.transform.CompareTag("smallCircles")) {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            collision.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            smallSync = true;
            CheckSynchronization(collision.gameObject.transform.root.gameObject); //Le pasamos el gameObject del padre, el objeto que contiene toda la estructura sync
        }
    }

    void PlayerDie() {
        if(GameController.instance.gameState == GameState.PLAYING) {
            GetComponent<CircleCollider2D>().isTrigger = false;
            GameController.instance.gameState = GameState.GAMEOVER;
        }
    }

    void CheckSynchronization(GameObject syncStructure) {
        if(IsSynchronizationCompleted(syncStructure) && GameController.instance.gameState == GameState.PLAYING) {
            GameController.instance.NewSynchronization(syncStructure);

            Destroy(syncStructure);

            bigSync = smallSync = false;
        }
    }

    bool IsSynchronizationCompleted(GameObject syncStructure) {
        bool synchroCompleted = false;

        if((syncStructure.transform.CompareTag("easySync") ||
            syncStructure.transform.CompareTag("sawSync") ||
            syncStructure.transform.CompareTag("dobleSawSync"))
            && smallSync) {
            synchroCompleted = true;
        } else if(syncStructure.transform.CompareTag("teleSync") && smallSync && bigSync) {
            TeleportPlayer(syncStructure);
            synchroCompleted = true;
        } else if(smallSync && bigSync) {
            synchroCompleted = true;
        }

        return synchroCompleted;
    }

    void TeleportPlayer(GameObject syncStructure) {
        //Si hay otro "teleSync" distinto al pasado por parametro, teletransportamos el Player a el
        GameObject[] teleportSyncs = GameObject.FindGameObjectsWithTag("teleSync");

        for(int i = 0; i < teleportSyncs.Length; i++) {
            if(teleportSyncs[i] != syncStructure) {
                //Teleportamos player
                transform.position = new Vector3(transform.position.x, teleportSyncs[i].transform.position.y, transform.position.z);
                break;
            }
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
