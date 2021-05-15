using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject playerGameObject;
    public Text hpText;
    public Text scoreText;
    int score;
    public bool isGameOver;

    MovementProvider moveProvider;

    AudioSource musicSource;
    public AudioClip musicClip;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isGameOver = false;
        moveProvider = GetComponent<MovementProvider>();
        musicSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        moveProvider.StartMove();
        musicSource.PlayOneShot(musicClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            hpText.text = "HP : " + (int)playerGameObject.GetComponent<PlayerController>().hp;
            scoreText.text = "Score : " + (int)score;
        }
    }

    public void GetScored(int value)
    {
        score += value;
    }

    public void EndGame()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
