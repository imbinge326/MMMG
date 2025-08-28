using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject PauseUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backToMenuButton.onClick.AddListener(BackToMenu);
        settingButton.onClick.AddListener(Pause);
        continueButton.onClick.AddListener(Continue);
    }

    // Update is called once per frame
    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Pause()
    {
        PauseUI.SetActive(true);
    }

    private void Continue()
    {
        PauseUI.SetActive(false);
    }
}
