using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject[] _stageSpawners;
    [SerializeField] private Transform[] _stagePlayerSpawner;
    private int _currentStage = 0;
    private List<Enemy> _enemies = new();
    private bool _stageStarted;

    public void AddEnemy(Enemy enemy)
    {
        enemy.Dead += RemoveEnemy;
        _stageStarted = false;
        _enemies.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemy.Dead -= RemoveEnemy;
        _enemies.Remove(enemy);
        if(_enemies.Count == 0)
            _stageStarted = false;
    }

    private void Update()
    {
        if (_stageStarted)
        {
            return;
        }
        if(_enemies.Count == 0) 
        {
            _stageStarted = true;
            _player.ApplyHeal(5);
            _currentStage += 1;              
            _player.transform.position = _stagePlayerSpawner[_currentStage - 1].position;
            _stageSpawners[_currentStage - 1].SetActive(true);
        }
    }

}
