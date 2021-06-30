using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player _player;
    private World _world;
    [HideInInspector] private int _points;
    [HideInInspector] public bool _inGameOver;
    [SerializeField] private float speedFood = 1f;
    [SerializeField] public int maxFood = 100;
    [SerializeField] private TMP_Text _pointsGUI_Play;
    [SerializeField] private TMP_Text _pointsGUI_GameOver;
    [SerializeField] private GameObject _GameOverGUI;


    private void Start()
    {
        _world = FindObjectOfType<World>();
        _player = FindObjectOfType<Player>();
    }

    public void AddPoints(int value)
    {
        _points += value;
        RefreshGUI_Play();
    }

    private void RefreshGUI_Play()
    {
        _pointsGUI_Play.text = _points.ToString();
    }

    public void ActiveGameOverGUI()
    {
        _inGameOver = true;
        _GameOverGUI.SetActive(true);
        RefreshGUI_GameOver();
    }

    private void RefreshGUI_GameOver()
    {
        _pointsGUI_GameOver.text = _points.ToString();
    }

    public void RestartGame()
    {
        if (_inGameOver && Input.GetKey(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        _GameOverGUI.SetActive(false);
        _inGameOver = false;
        _points = 0;
        _player.ResetSpeed();
        RestartFoods();
        RefreshGUI_Play();
    }

    private void RestartFoods()
    {
        for (int i = 1; i < _world.transform.childCount; i++)
        {
            Destroy(_world.transform.GetChild(i).gameObject);
        }
        _world.NewFood();
        _world.NewPoison();
    }

    private void Update()
    {
        RestartGame();
    }

    public void IncreaseDifficulty(Transform food)
    {
        _player.AddSpeed(speedFood);
        if (_points % 5 == 0)
        {
            _world.NewPoison(food);
        }
        _world.NewFood();
    }
}
