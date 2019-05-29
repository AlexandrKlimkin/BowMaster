using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton_Temp : MonoBehaviour
{
    private Button _Button;

    private void Awake()
    {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        _Button.onClick.RemoveAllListeners();
    }
}
