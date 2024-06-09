using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject[] _stageSpawners;

    [SerializeField] private Transform[] _stagePlayerSpawner;
    private int _stage = 1;
    private List<Enemy> _enemies = new();
    private bool _start = true;


    public void AddEnemy(Enemy enemy)
    {
        _start = false;
        Debug.Log(enemy);
        _enemies.Add(enemy);
        Debug.Log(_enemies.Count);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void Update()
    {
        if (_start == true)
        {
            return;
        }
        if(_enemies.Count == 0) 
        { 
            if ( _stage == 1)
            {
                _player.transform.position = _stagePlayerSpawner[0].position;
                _stageSpawners[0].SetActive(true);
            }
            if (_stage == 2)
            {
                _player.transform.position = _stagePlayerSpawner[1].position;
                _stageSpawners[1].SetActive(true);
            }
            if (_stage == 3)
            {
                _player.transform.position = _stagePlayerSpawner[2].position;
                _stageSpawners[2].SetActive(true);
            }
            if (_stage == 4)
            {
                _player.transform.position = _stagePlayerSpawner[3].position;
                _stageSpawners[3].SetActive(true);
            }

        }
    }

}
