using UnityEngine;
using Basement;
using Basement.Player;
using System.Collections;
using System;

public class ElevatorDoor : MonoBehaviour
{
    public Action onCompleteOpenDoor;
    public Action onCompleteCloseDoor;

    private Animator _animator;
    private Elevator _elevator;
    private Collider2D _collider;
    private bool _isDoorOpen = false;       //���� FSM ���� ����

    private int _doorOpenHash = Animator.StringToHash("Open");
    private int _doorCloseHash = Animator.StringToHash("Close");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false; 
        _isDoorOpen = false;
    }

    public void OpenDoor()
    {
        //Door Open Animation
        _animator.SetTrigger(_doorOpenHash);
    }

    public void CompleteOpenDoor()
    {
        //On Complete Door Open Animation
        _isDoorOpen = true;
        _collider.enabled = true;
        onCompleteOpenDoor?.Invoke();
    }

    public void CloseDoor()
    {
        _animator.SetTrigger(_doorCloseHash);
        _collider.enabled = false;
        _isDoorOpen = false;
    }

    public void CompleteCloseDoor()
    {
        onCompleteCloseDoor?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //Enter elevator when door opened change floor
        if (_isDoorOpen == false) return;   //If door is not opened return

        Debug.Log(_collider.enabled);
        _elevator.ChangeFloor(this);

        //TODO : block player input until elevator door open
    }

    public void Init(Elevator elevator)
    {
        _elevator = elevator;
    }
}
