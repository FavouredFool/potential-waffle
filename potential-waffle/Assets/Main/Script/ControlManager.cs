using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ControlManager : MonoBehaviour
{
    [SerializeField] GameObject _startScreen;
    [SerializeField] GameObject _endScreen;
    [SerializeField] GameObject _hud;
    [SerializeField] GameObject _skillTree;


    GameObject _ship;

    void Start()
    {
        _hud.SetActive(false);
        _startScreen.SetActive(true);
        Time.timeScale = 0;
        _ship = GameObject.FindGameObjectWithTag("Ship");
    }
    
    void Update()
    {
        if (_ship == null)
        {
            _hud.SetActive(false);
            _endScreen.SetActive(true);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        _hud.SetActive(true);
        _startScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToggleSkillTree()
    {
        _skillTree.SetActive(!_skillTree.activeSelf);

        if (_skillTree.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
