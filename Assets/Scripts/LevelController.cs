using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    [SerializeField] float currentLevel;
    void Awake()
    {
        if (currentLevel > 0) PlayerPrefs.SetInt("level", (int)currentLevel);
        if (PlayerPrefs.GetInt("level") == 0) PlayerPrefs.SetInt("level", 1);
        currentLevel = PlayerPrefs.GetInt("level");
        if (currentLevel > levels.Length)
        {
            currentLevel--;
            PlayerPrefs.SetInt("level", (int)currentLevel);
        }
        Instantiate(levels[(int)currentLevel - 1]);
    }
}
