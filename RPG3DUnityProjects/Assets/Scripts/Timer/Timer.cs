using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float duration = 120f;
    float currentTime = 0;
    bool timerStart = false;

    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] Button timerButton;

    static Timer instance;

    public static Timer Instance => instance;

    public UnityEvent OnTimeout = new UnityEvent();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        SetButtonOnClick();
    }

    public void StartTimer()
    {
        timerStart = true;
        currentTime = duration;
        UpdateTimerText(currentTime); // Update UI immediately
    }

    public void StopTimer()
    {
        timerStart = false;
        currentTime = 0;
        UpdateTimerText(0); // Optional: reset text to 00:00
    }

    private void Update()
    {
        if (timerStart)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                Debug.Log("Time Out!");
                StopTimer();

                if(timerButton != null)
                {
                    timerButton.gameObject.SetActive(true);
                }
            }

            UpdateTimerText(currentTime);
        }
    }

    private void UpdateTimerText(float time)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = $"{minutes:D2}:{seconds:D2}";
        }

    }

    private void SetButtonOnClick()
    {
        if (timerButton != null)
        {
            timerButton.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        timerButton.gameObject.SetActive(false);
        StartTimer();
    }

    public void TimerLoseDetection()
    {
        if(currentTime > 0)
        {
            Debug.Log("NPC Enter Room before 2minutes, you LOSE!");
            StopTimer();
            if(timerText != null)
            {
                timerText.text = "YOU LOSE!!";
            }

            if(timerButton != null)
            {
                timerButton.gameObject.SetActive(true);
            }
        }
    }
}
