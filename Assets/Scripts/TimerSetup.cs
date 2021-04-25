using UnityEngine;
using UnityEngine.UI;

public class TimerSetup : MonoBehaviour
{
    [SerializeField] private Text timeText;

    private float initialTime;

    private void OnEnable()
    {
        initialTime = Time.time;
    }

    private void Update()
    {
        float timer = (Time.time - initialTime);
        float seconds = Mathf.FloorToInt(timer % 60);
        float minute = Mathf.FloorToInt(timer / 60);
        timeText.text = minute.ToString() + ":" + seconds.ToString();
    }
}
