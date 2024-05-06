using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{
    EnemyController _enemyController;
    public EnemyPatrolState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Enter()
    {
        _enemyController.Patrol();
    }

    public void Execute()
    {
        if(_enemyController.isReactTarget())
            _enemyController.Patrol();
    }

    public void Exit()
    { 
    }
}

public class EnemyReturnOrigin: IState
{
    public EnemyController _enemyController;

    public EnemyReturnOrigin(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }
    public void Enter()
    {
        _enemyController.homeComing();
    }

    public void Execute()
    {
        if (_enemyController.isReactTarget())
            _enemyController.Patrol();
    }

    public void Exit()
    {
         
    }
}
public class EnemyChaserState : IState
{
    EnemyController _controller;
    public EnemyChaserState(EnemyController enemyController)
    {
        _controller = enemyController;
    }
    public void Enter()
    {
        
    }

    public void Execute()
    {
         this._controller.Chaser();
    }

    public void Exit()
    {
        
    }
}
