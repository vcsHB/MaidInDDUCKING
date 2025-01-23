using Agents;
using InputManage;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = InputManage.PlayerInput;

namespace Basement.Player
{
    public class BasementPlayer : Agent
    {
        [field: SerializeField] public PlayerInput playerInput { get; private set; }
        public event Action onInteract;

        private float _xVelocity;
        private bool _wallDetected;
        private bool _readyInteract;

        private void FixedUpdate()
        {
            Move();
            Interact();
        }

        private void Move()
        {
            float preveDir = _xVelocity;
            _xVelocity = playerInput.InputDirection.x;


        }

        private void Interact()
        {
            //���߿� ����ǲ���� Ű �߰��ؼ� �ٲ������
            //Update���� �����Ű�� ���� �������� �����ϰ� �ٲ���
            if (Keyboard.current.fKey.wasPressedThisFrame)
                onInteract?.Invoke();
        }
    }
}
