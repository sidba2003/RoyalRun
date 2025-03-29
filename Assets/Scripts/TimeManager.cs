using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Tooltip("Text area for the time manager UI")]
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float startTimeLeft;
    [SerializeField] Image fadeOutEffect;
    [SerializeField] GameObject gameOver;
    [SerializeField] float fadeOutEffectDuration;
    [SerializeField] GameObject restartButton;

    float timeLeft;
    bool fadeCalled = false;
    bool gameFinished = false;
    public static TimeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timeLeft = startTimeLeft;
        text.text = timeLeft.ToString("F1");
        Time.timeScale = 1f;
    }
    public bool getGameOver()
    {
        return gameFinished;
    }

    public void increaseTimer(float time)
    {
        timeLeft += time;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft >= 0) text.text = timeLeft.ToString("F1");

        if (timeLeft <= 0)
        {
            gameFinished = true;
            Time.timeScale = 0.1f;
            gameOver.SetActive(true);

            if (!fadeCalled)
            {
                StartCoroutine(FadeOutEffect());
                fadeCalled = true;
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator FadeOutEffect()
    {
        Image fadeImage = fadeOutEffect.GetComponent<Image>();
        Color imC = fadeImage.color;

        float time = 0;

        while (time <= fadeOutEffectDuration)
        {
            time += (Time.deltaTime * 10);
            float aVal = Mathf.Lerp(0, 1, time / fadeOutEffectDuration);

            imC.a = aVal;
            fadeImage.color = imC;

            yield return null;
        }

        imC.a = 1;
        fadeImage.color = imC;

        restartButton.SetActive(true);
    }
}
