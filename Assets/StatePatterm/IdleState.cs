using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateTest {
    public class IdleState : IState
    {
        PlayerController _controller;
        public IdleState(PlayerController player) {
            _controller = player;
        }
        public void Enter()
        {
            Debug.Log("Idle Enter State");
        }

        public void Execute()
        {
            Debug.Log("Idle Execute State");
        }

        public void Exit()
        {
            Debug.Log("Idle Exit State");
        }
    }


    public class MovingState : IState
    {
        PlayerController _controller; 
       
        public MovingState(PlayerController player)
        {
            _controller = player; 
        }
        public void Enter()
        {
            Debug.Log("Moving Enter State");
            _controller.GetComponent<Rigidbody>().isKinematic = false;
            _controller.GetComponent<MeshRenderer>().material.color = Color.white;
        }

        public void Execute()
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                //Moving object
                Vector3 _movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                _controller.Moving(_movement);
                return;
            } 
        }

        public void Exit()
        {
            Debug.Log("Moving Exit State");
        }
    }

    public class FlyingState : IState
    {

        PlayerController _controller;
        public FlyingState(PlayerController player)
        {
            _controller=player; 
        }
        public void Enter()
        {
            _controller.GetComponent<Rigidbody>().isKinematic = true;
            _controller.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public void Execute()
        {
            Debug.Log("Flying statee");
            Vector3 _movement = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            { 
               _movement = new Vector3(0,0, Input.GetAxisRaw("Vertical"));
                _controller.Flying(_movement);
                return;
            } 

            if (Input.GetKey(KeyCode.W) )
            {
                _movement = new Vector3(0,1,0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                _movement = new Vector3(0, -1, 0);
            }

            _controller.Flying(_movement);
        }

        public void Exit()
        {
             
        }
    }
}