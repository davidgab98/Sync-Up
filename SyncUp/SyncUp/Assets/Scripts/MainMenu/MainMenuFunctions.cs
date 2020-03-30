using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour {

    public GameObject configWall, tutorialWall, shareWall, rankingWall;

    bool wallOpened;

    private void Start() {
        PrepareWalls();
    }

    void PrepareWalls() {
        configWall.SetActive(false);
        tutorialWall.SetActive(false);
        shareWall.SetActive(false);
        rankingWall.SetActive(false);
    }

    public void PlayGame() {
        if(!wallOpened) {
            SceneManager.LoadScene("MainGame");
        }
    }

    public void OpenTutorial() {
        if(!wallOpened) {
            tutorialWall.SetActive(true);
            wallOpened = true;
        }
    }

    public void OpenConfig() {
        if(!wallOpened) {
            configWall.SetActive(true);
            wallOpened = true;
        }
    }

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

    public void CloseTutorial() {
        tutorialWall.SetActive(false);
        wallOpened = false;
    }

    public void CloseConfig() {
        configWall.SetActive(false);
        wallOpened = false;
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
