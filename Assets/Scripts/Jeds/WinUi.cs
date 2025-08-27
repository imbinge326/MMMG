using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUi : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
