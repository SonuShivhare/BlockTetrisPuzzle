using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject partition;
    [SerializeField] public RectTransform rects;
    [SerializeField] public RectTransform slots;

    public void PlayButton()
    {
        SetupTetromino();
        Invoke(nameof(StartTimer), 2f);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetActiveTrue(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void SetActiveFalse(GameObject panel)
    {
        panel.SetActive(false);
    }

    private void SetupTetromino()
    {
        foreach (RectTransform rect in rects)
        {
            foreach (RectTransform slot in slots)
            {
                if (rect.GetComponent<Tetromino>().GetID() == slot.GetComponent<Tetromino>().GetID())
                {
                    if (rect.anchoredPosition != slot.anchoredPosition)
                    {
                        rect.GetComponent<Tetromino>().SmothTransition(slot.anchoredPosition);
                    }
                    break;
                }
            }
        }
    }

    private void SlideUp()
    {
        foreach (RectTransform rect in rects)
        {
            rect.GetComponent<Tetromino>().isGlowUp = true;
        }
    }

    private bool GameOver()
    {
        foreach (RectTransform rect in rects)
        {
            if (rect.GetComponent<Tetromino>().isFinalPos == false) return false;
        }
        return true;
    }

    private void StartTimer()
    {
        timer.SetActive(true);
    }

    private void LateUpdate()
    {
        if (GameOver())
        {
            partition.SetActive(false);
            timer.SetActive(false);
            gameOverPanel.SetActive(true);
            SlideUp();
        }
    }
}
