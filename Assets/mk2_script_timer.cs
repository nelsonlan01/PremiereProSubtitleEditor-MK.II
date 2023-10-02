using UnityEngine;
using UnityEngine.UI;

public class mk2_script_timer : MonoBehaviour{
    private float elapsedTime;
    private bool isRunning;
    public Text timerText;
    public Button startButton;
    public Button timestampButton;
    public InputField inputField;
    public InputField outputField;
    private static int i = 0;
    private string previousTimestamp = "00:00:00,000";

    private void Start()
    {
        elapsedTime = 0f;
        isRunning = false;

        startButton.onClick.AddListener(ToggleTimer);
        timestampButton.onClick.AddListener(GetTimestamp);
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void ToggleTimer()
    {
        isRunning = !isRunning;

        if (isRunning)
        {
            startButton.GetComponentInChildren<Text>().text = "Stop";
        }
        else
        {
            startButton.GetComponentInChildren<Text>().text = "Start";
        }
    }

    private void GetTimestamp()
    {
        string[] lines = inputField.text.Split('\n');
        if (i < lines.Length)
        {
            FormatAndAppendTimer(lines[i]);
        }
        i++;
    }

    private void FormatAndAppendTimer(string line)
    {
        string currentTimestamp = timerText.text;
        string output = string.Format("{0}\n{1} --> {2}\n{3}", i, previousTimestamp, currentTimestamp, line);
        outputField.text += output + "\n\n";
        previousTimestamp = currentTimestamp;
    }

    private void UpdateTimerText()
    {
        int hours = (int)(elapsedTime / 3600f);
        int minutes = (int)((elapsedTime % 3600f) / 60f);
        int seconds = (int)(elapsedTime % 60f);
        int milliseconds = (int)((elapsedTime * 1000f) % 1000f);

        string timerTextFormatted = string.Format("{0:00}:{1:00}:{2:00},{3:000}", hours, minutes, seconds, milliseconds);

        timerText.text = timerTextFormatted;
    }
}