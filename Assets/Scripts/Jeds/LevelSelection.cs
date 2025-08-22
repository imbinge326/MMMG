using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class LevelSelection : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private RectTransform level1;
    [SerializeField] private RectTransform level2;
    [SerializeField] private RectTransform level3;
    [SerializeField] private RectTransform level4;

    private RectTransform[] options;

    void Awake()
    {
        options = new RectTransform[] {level1, level2, level3, level4};
    }

    void OnEnable()
    {
        // Kill any existing animations first
        foreach (RectTransform option in options)
        {
            option.DOKill();
        }

        //first
        level1.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutQuad);
        //second
        level2.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.1f);
        //third
        level3.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.2f);
        //fourth
        level4.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.3f);
    }

    private void OnDisable()
    {
        // Kill animations before setting scale
        foreach (RectTransform option in options)
        {
            option.DOKill();
            option.localScale = Vector3.zero;
        }
    }

    public void Level1Button()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2Button()
    {
        SceneManager.LoadScene(2);
    }

    public void Level3Button()
    {
        SceneManager.LoadScene(3);
    }

    public void Level4Button()
    {
        SceneManager.LoadScene(4);
    }

}
