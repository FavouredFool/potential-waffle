using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ControlManager : MonoBehaviour
{
    [SerializeField] GameObject _startScreen;
    [SerializeField] GameObject _endScreen;

    GameObject _ship;

    void Start()
    {
        Time.timeScale = 0;
        _ship = GameObject.FindGameObjectWithTag("Ship");
    }
    
    void Update()
    {
        if (_ship == null)
        {
            _endScreen.SetActive(true);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        _startScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
