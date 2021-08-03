using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private GameEventController gameEvent;
    [SerializeField] private BallManager ballManager;
    [SerializeField] private AudioContoller audioController;
    [SerializeField] private ButtonContoller buttonController;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private JoyStick joyStick;
    private bool isGameStart = false;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        Screen.SetResolution(1920, 1080, true);
        joyStick.isJoyStickCanUse = false;
        buttonController.EnableButton(buttonController.buttons.Jump, false);
        buttonController.EnableButton(buttonController.buttons.Crouch, false);
        buttonController.EnableButton(buttonController.buttons.ReStatrButton, false);
        buttonController.EnableButton(buttonController.buttons.StartButton, true);
    }

    private void Update()
    {
        if (isGameStart)
        {
            if (playerManager.IsPlayerDead()) GameOver();
        }
    }

    public void StartGame()
    {
        joyStick.isJoyStickCanUse = true;
        buttonController.EnableButton(buttonController.buttons.Jump, true);
        buttonController.EnableButton(buttonController.buttons.Crouch, true);
        buttonController.EnableButton(buttonController.buttons.StartButton, false);
        audioController.PlayOneShotSound(audioController.m_audioClip.StartSound);
        timer.StartTimer();
        gameEvent.StartCheckGameEvent();
        playerManager.RespawnPlayer(1);
        isGameStart = true;
    }

    private void GameOver()
    {
        Debug.Log("Dead");
        isGameStart = false;
        audioController.PlayOneShotSound(audioController.m_audioClip.GameOverSound);
        buttonController.EnableButton(buttonController.buttons.ReStatrButton, true);
        timer.StopCounting();
    }

    public void ReStartGame()
    {
        buttonController.EnableButton(buttonController.buttons.StartButton, false);
        audioController.PlayOneShotSound(audioController.m_audioClip.ReStartSound);
        SceneManager.LoadScene(0);
    }
}
