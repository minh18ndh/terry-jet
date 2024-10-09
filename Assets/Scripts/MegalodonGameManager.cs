using Game.Scene1;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MegalodonGameManager : MonoBehaviour
{
    public static MegalodonGameManager Instance;
    //[SerializeField] TextMeshProUGUI endgameText;
    [SerializeField] TMP_Text scoreText;
    //[SerializeField] Button restartBtn;
    [SerializeField] SpriteRenderer scenePassBackground;
    //[SerializeField] TextMeshProUGUI eraTimeText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;

    float time;
    public float runSpeed;
    private float score;
    public float START_TIME = 5.33f;
    public float END_TIME = 2.58f;
    private void Start()
    {
        Instance = this;
        time = START_TIME;
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(time < END_TIME)
        {
            triggerEndScene();
        }
        else
        {
            time -= Time.deltaTime * runSpeed;
            score = time;
            //scoreText.text = Mathf.FloorToInt(score).ToString("D7");
            scoreText.text = score.ToString("F4");
        }
    }

    public void endGame()
    {
        //endgameText.text = "You died!";
        Time.timeScale = 0;
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        //SceneManager.LoadScene("T-Rex Scene");
    }

    public void triggerEndScene()
    {
        Time.timeScale = 0;
        Color color = scenePassBackground.color;
        if (color.a < 0.3f)
        {
            color.a += 0.002f;
            scenePassBackground.color = color;
        }

        else
        {
            // Once the fade-out effect is done, load the Jet scene
            SceneManager.LoadScene("Jet Scene");
        }
    }

    public bool IsSceneEnd()
    {
        return time < END_TIME;
    }

    public void NewGame()
    {
        Thread.Sleep(1000);
        Time.timeScale = 1;
        SceneManager.LoadScene("T-Rex Scene");
    }
}
