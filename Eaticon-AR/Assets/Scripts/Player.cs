using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private World _world;
    private GameManager _gameManager;
    [SerializeField] private GameObject _player;
    public float _speed = 40.0f;

    private void Start()
    {
        _world = FindObjectOfType<World>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void AddSpeed(float addSpeed)
    {
        if (_speed <= 100f)
        {
            _speed += (addSpeed / 2);
            _world.SetSpeed(_speed / 2);
        }
    }

    public void ResetSpeed()
    {
        _speed = 40.0f;
        _world.SetSpeed(_speed / 2);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_gameManager._inGameOver == false)
        {
            float vertical = _speed * Input.GetAxis("Vertical") * Time.deltaTime;
            float horizontal = _speed * Input.GetAxis("Horizontal") * Time.deltaTime;

            var rotationEuler = new Vector3(vertical, -horizontal, 0);
            _player.transform.Rotate(_speed * rotationEuler * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            var food = other.transform.GetComponent<Food>();
            food.StartAnimationDestroy();
            _gameManager.AddPoints(food.value);
            _gameManager.IncreaseDifficulty();
        }
        if (other.tag == "Poison")
        {
            _gameManager.ActiveGameOverGUI();
            _world.NewPoison();
        }
        Destroy(other.transform.parent.gameObject, 1f);
    }
}
