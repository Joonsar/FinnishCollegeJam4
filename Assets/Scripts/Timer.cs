using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 600f; // 10 minutes in seconds
    public TextMeshProUGUI timerText;
    [SerializeField] Animation timerAnimation;

    //private void Start()
    //{
    //    timerAnimation = GetComponent<Animation>();
    //}
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.FloorToInt(timeRemaining / 60f).ToString("00") + ":" + Mathf.FloorToInt(timeRemaining % 60f).ToString("00");
        }
        else
        {
            // End the game
            SceneManager.LoadScene("GameOver"); // Need to Build GameOverScene
        }
        if (timeRemaining <= 10)
        {
            // Play the animation
            timerAnimation.Play();
        }
    }
}
