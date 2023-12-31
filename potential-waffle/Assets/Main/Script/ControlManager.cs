using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;

public class ControlManager : MonoBehaviour
{
    [SerializeField] GameObject _startScreen;
    [SerializeField] GameObject _endScreen;
    [SerializeField] GameObject _hud;
    [SerializeField] GameObject _skillTree;
    [SerializeField] EnemySpawner _spawner;
    [SerializeField] Text _timer;
    [SerializeField] Button _menuButton;
    [SerializeField] RectTransform _dashedLine;
    [SerializeField] TMP_Text _aliensKilledText;

    GameObject _ship;
    float _startTime = 0;
    int _aliensKilled = 0;
    TimeSpan _timePlaying;

    AudioManager audio;

    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        _hud.SetActive(false);
        _startScreen.SetActive(true);
        _skillTree.SetActive(false);
        Time.timeScale = 0;
        _ship = GameObject.FindGameObjectWithTag("Ship");

        audio.ChangeVolume("BackgroundMusic", 0.15f);
        
    }
    
    void Update()
    {
        _aliensKilledText.text = "Aliens Killed:\n" + _aliensKilled.ToString();

        if (_ship == null)
        {
            audio.ChangeVolume("BackgroundMusic", 0.15f);
            _hud.SetActive(false);
            _endScreen.SetActive(true);
        }
        else
        {
            //_timePlaying = TimeSpan.FromSeconds(Time.time - _startTime);
            //_timer.text = _timePlaying.ToString("mm':'ss':'ff");

            if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.E))
            {
                ToggleSkillTree();
            }
        }
    }

    public void LoadGame()
    {
        audio.Play("UIButton");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        audio.ChangeVolume("BackgroundMusic", 0.5f);
        audio.Play("UIButton");
        _startTime = Time.time;
        _hud.SetActive(true);
        _startScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MoveLineUp()
    {
        _dashedLine.anchoredPosition += new Vector2(0, 170);
    }

    public void ToggleSkillTree()
    {
        audio.Play("UIButton");
        audio.ChangeVolume("Thruster", 0);
        _skillTree.SetActive(!_skillTree.activeSelf);


        if (_skillTree.activeSelf)
        {
            _menuButton.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            _menuButton.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void IncreaseAliensKilled()
    {
        _aliensKilled++;
    }
}
