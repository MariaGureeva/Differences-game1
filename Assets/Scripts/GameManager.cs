using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnDifferenceWasFound;
    public static GameManager instance;
    private int differencesFoundCount = 0;

    [Header("Timer Vertical")]
    [SerializeField] private Slider portraitTimeBarSlider;
    [SerializeField] private TextMeshProUGUI portraitTimeText;

    [Header("Timer Horizontal")]
    [SerializeField] private Slider landscapeTimeBarSlider;
    [SerializeField] private TextMeshProUGUI landscapeTimeText;

    [Header("General Settings")]
    [SerializeField] private float levelTimeInSeconds = 90f;
    [SerializeField] private int totalDifferencesToFind = 7;

    [Header("Timer Animation")]
    [SerializeField] private float pulseStartTime = 10f;
    [SerializeField] private Color normalTextColor = Color.white;
    [SerializeField] private Color warningTextColor = Color.red;
    [SerializeField] private float pulseSpeed = 5f;
    [SerializeField] private float pulseAmount = 0.1f;
    private Vector3 initialTextScale;

    [Header("UI Screens")]
    [SerializeField] private GameObject victoryScreenObject;
    [SerializeField] private GameObject timeOutScreenObject;

    private float currentTime;
    private bool isGameActive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTime = levelTimeInSeconds;
        isGameActive = true;

        if (victoryScreenObject != null)
        {
            victoryScreenObject.SetActive(false);
        }
        if (timeOutScreenObject != null)
        {
            timeOutScreenObject.SetActive(false);
        }

        if (portraitTimeText != null)
        {
            initialTextScale = portraitTimeText.transform.localScale;
            portraitTimeText.color = normalTextColor;
        }
        if (landscapeTimeText != null)
        {
            // Устанавливаем начальный масштаб для ландшафтного текста, если он вдруг отличается
            // Если он такой же, то можно использовать initialTextScale
            landscapeTimeText.color = normalTextColor;
        }
    }

    void Update()
    {
        if (!isGameActive)
        {
            return;
        }

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            currentTime = 0;
        }

        float sliderValue = currentTime / levelTimeInSeconds;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        string timeString = time.ToString(@"m\:ss");

        if (portraitTimeBarSlider != null)
        {
            portraitTimeBarSlider.value = sliderValue;
        }
        if (portraitTimeText != null)
        {
            portraitTimeText.text = timeString;
        }

        if (landscapeTimeBarSlider != null)
        {
            landscapeTimeBarSlider.value = sliderValue;
        }
        if (landscapeTimeText != null)
        {
            landscapeTimeText.text = timeString;
        }

        if (currentTime <= pulseStartTime)
        {
            float scaleOffset = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            Vector3 pulseScale = initialTextScale * scaleOffset;

            if (portraitTimeText != null)
            {
                portraitTimeText.transform.localScale = pulseScale;
                portraitTimeText.color = warningTextColor;
            }
            if (landscapeTimeText != null)
            {
                landscapeTimeText.transform.localScale = pulseScale;
                landscapeTimeText.color = warningTextColor;
            }
        }

        if (currentTime <= 0)
        {
            EndGame(false);
        }
    }

    public void ReportDifferenceFound()
    {
        if (!isGameActive) return;

        differencesFoundCount++;
        if (OnDifferenceWasFound != null)
        {
            OnDifferenceWasFound.Invoke(differencesFoundCount);
        }
        Debug.Log("Differences found: " + differencesFoundCount);

        if (differencesFoundCount >= totalDifferencesToFind)
        {
            EndGame(true);
        }
    }

    private void EndGame(bool didWin)
    {
        isGameActive = false;

        if (didWin)
        {
            Debug.Log("Victory!");
            if (victoryScreenObject != null)
            {
                victoryScreenObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Time out!");
            if (timeOutScreenObject != null)
            {
                timeOutScreenObject.SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}