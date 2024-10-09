using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Game.Scene1;
using UnityEngine.SceneManagement;
using System.Threading;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float START_TIME = 72.7f;
    public float END_TIME = 66.0f;
    public float time;
    private float bottomEdge;
    public float gameSpeed { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Spawner spawner;

    private Player player;
    public GameObject asteroidPrefab; // Keep the prefab reference
    private GameObject asteroidInstance; // Variable to hold the instantiated asteroid

    private float score;
    public float Score => score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            time = START_TIME;
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        time = START_TIME;
        NewGame();
    }

    public void NewGame()
    {
        Obstacle1[] obstacles = FindObjectsOfType<Obstacle1>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        if (asteroidInstance != null)
        {
            Destroy(asteroidInstance);
            asteroidInstance = null; // Optional: Clear the reference
        }

        time = START_TIME;
        player.transform.position = new Vector3(-7.85f, 1, 0);
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (time <= END_TIME)
        {
            time = END_TIME;

            if (asteroidInstance == null)
            {
                asteroidInstance = Instantiate(asteroidPrefab, new Vector2(12, 8), Quaternion.identity);
            }

            bottomEdge = -12f;

            if (asteroidInstance.transform.position.x < bottomEdge)
            {
                // Freeze the asteroid's position by making its Rigidbody2D kinematic
                Rigidbody2D rb = asteroidInstance.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Freeze the physics behavior
                }
                StartCoroutine(ScaleAsteroid());

                if (asteroidInstance.transform.localScale.x >= 9f)
                {
                    triggerEndScene();
                }
            }
        }
        else
        {
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
            time -= initialGameSpeed * 0.1f * Time.deltaTime;
            score = time;
            scoreText.text = score.ToString("F4");
        }
    }

    private IEnumerator ScaleAsteroid()
    {
        // Gradually scale the x component of the asteroid to 10
        Vector3 targetScale = new Vector3(10f, 10f, asteroidInstance.transform.localScale.z);
        while (asteroidInstance.transform.localScale.x < 9f)
        {
            asteroidInstance.transform.localScale = Vector3.Lerp(asteroidInstance.transform.localScale, targetScale, 0.04f * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }

    public void triggerEndScene()
    {
        Thread.Sleep(1000);
        Time.timeScale = 0;
        SceneManager.LoadScene("MegalodonScene");
    }
}
