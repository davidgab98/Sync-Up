using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { START, PLAYING, PAUSE, RESUME, GAMEOVER, WIN };

public class GameController : MonoBehaviour {
    public static GameController instance;

    // GAME STATE VARIABLES
    public GameState gameState = GameState.START;
    public GameObject pauseWall, gameoverWall, winWall, startWall, gameWall;

    // PLAYER
    public float jumpForce = 5;
    GameObject player;

    // SYNCS
    public float rideSyncsSpeed = 3;
    public float rotatingSpeed = 5;

    // LIMITS DISTANCES
    public float horizontalLimitLeft, horizontalLimitRight;


    public void Awake() {
        //Instance
        if(GameController.instance == null) {
            GameController.instance = this;
        } else if(GameController.instance != this) {
            Destroy(gameObject);
            Debug.LogError("GameController instantiate for 2 time. This wouldn't has to happen.");
        }

        //Player
        player = GameObject.FindGameObjectWithTag("player");
    }

    void Start() {
        PrepareWalls();
        gameState = GameState.START;
        Time.timeScale = 1;
    }

    void PrepareWalls() {
        startWall.SetActive    (true);
        pauseWall.SetActive    (false);
        gameoverWall.SetActive (false);
        winWall.SetActive      (false);
        gameWall.SetActive     (false);
    }

    void Update() {
        if(gameState == GameState.START) {
            CheckStart();
        } else if(gameState == GameState.PAUSE) {
            Pause();
        }else if(gameState == GameState.WIN) {
            Win();
        }else if(gameState == GameState.GAMEOVER) {
            GameOver();
        } else if(gameState == GameState.RESUME) {
            Resume();
        } else if(gameState == GameState.PLAYING) {
            
        }
    }

    void CheckStart() {
        if(Input.GetMouseButtonDown(0)) {
            //Player as dynamic and add up impulse
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            player.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            //States things
            gameState = GameState.PLAYING;
            startWall.SetActive(false);
            gameWall.SetActive(true);
        }
    }

    void Pause() {
        gameWall.SetActive(false);
        pauseWall.SetActive(true);
    }

    void Resume() {
        pauseWall.SetActive(false);
        gameWall.SetActive(true);
        gameState = GameState.PLAYING;
    }

    void GameOver() {
        gameWall.SetActive(false);
        gameoverWall.SetActive(true);
    }
    void Win() {
        gameWall.SetActive(false);
        winWall.SetActive(true);
    }

    private void OnDestroy() {
        if(GameController.instance == this) {
            GameController.instance = null;
        }
    }
}
