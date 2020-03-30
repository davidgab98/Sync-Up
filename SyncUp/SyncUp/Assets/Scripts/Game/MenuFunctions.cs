using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    //Walls
    public GameObject rankingWall, shareWall; 
    bool wallOpened = false;

    public void Pause() {
        Time.timeScale = 0;
        GameController.instance.gameState = GameState.PAUSE;
    } 

    public void Resume() {
        Time.timeScale = 1;
        GameController.instance.gameState = GameState.RESUME;
    }

    public void Restart() {
        SceneManager.LoadScene("MainGame");
    }

    public void ContinueAfterWin() {
        SceneManager.LoadScene("MainGame"); //TEMP
    }

    public void backToHome() {
        SceneManager.LoadScene("MainMenu");
    }


//WALLS: Tutorial, Config, Ranking and Share
    public void OpenShare() {
        if(!wallOpened) {
            shareWall.SetActive(true);
            wallOpened = true;
        }
    }

    public void OpenRanking() {
        if(!wallOpened) {
            rankingWall.SetActive(true);
            wallOpened = true;
        }
    }

    public void CloseShare() {
        shareWall.SetActive(false);
        wallOpened = false;
    }

    public void CloseRanking() {
        rankingWall.SetActive(false);
        wallOpened = false;
    }

}
