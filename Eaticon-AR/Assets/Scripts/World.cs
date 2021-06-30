using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _world;
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private GameObject _poisonPrefab;
    private float _speed = 20.0f;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        NewFood();
        NewPoison();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        MoveWorld();
    }

    private void MoveWorld()
    {
        if (_gameManager._inGameOver == false)
        {
            float vertical = _speed * Input.GetAxis("Vertical") * Time.deltaTime;
            float horizontal = _speed * Input.GetAxis("Horizontal") * Time.deltaTime;

            var rotationEuler = new Vector3(-vertical, +horizontal, 0);
            _world.transform.Rotate(_speed * rotationEuler * Time.deltaTime, Space.World);
        }
    }

    public void NewFood()
    {
        if (_gameManager.maxFood > transform.childCount)
        {
            var rotationEuler = new Vector3(Random.Range(0f, 359f), Random.Range(0f, 359f), 0);
            Instantiate(_foodPrefab, transform.position, Quaternion.Euler(rotationEuler), transform);
        }
    }

    public void NewPoison()
    {
        if (_gameManager.maxFood > transform.childCount)
        {
            var rotationEuler = new Vector3(Random.Range(0f, 359f), Random.Range(0f, 359f), 0);
            Instantiate(_poisonPrefab, transform.position, Quaternion.Euler(rotationEuler), transform);
        }
    }
    public void NewPoison(Transform food)
    {
        if (_gameManager.maxFood > transform.childCount)
        {
            var rotationEuler = new Vector3(food.rotation.eulerAngles.x + 90, food.rotation.eulerAngles.y + 90, 0);
            Instantiate(_poisonPrefab, transform.position, Quaternion.Euler(rotationEuler), transform);
        }
    }

}
