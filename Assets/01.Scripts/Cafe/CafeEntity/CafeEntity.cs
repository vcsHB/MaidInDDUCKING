using Agents.Animate;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    public abstract class CafeEntity : MonoBehaviour
    {
        public CafeEntityStateMachine stateMachine;
        public CafeEntityRenderer npcRenderer;
        public CafeEntitySO npcSO;

        public Action onCompleteMove;
        public float LookDir { get; protected set; } = 1;
        public Transform MoveTarget { get; protected set; }


        private Rigidbody2D _rigid;
        [HideInInspector] public int curAnimParam;

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>(); ;
            stateMachine = new CafeEntityStateMachine(this);

            npcSO.entityStateList.ForEach(state => stateMachine.AddState(state.stateName, state.className, state.animParam));
            stateMachine.ChangeState(npcSO.entityStateList[0].stateName);
        }

        protected virtual void Update()
        {
            stateMachine.currentState.UpdateState();
        }

        public void SetMoveTarget(Transform target)
            => MoveTarget = target;

        public virtual void Move(float dir)
        {
            if (Mathf.Sign(dir) != Mathf.Sign(LookDir))
                Flip();

            _rigid.linearVelocityX = dir * npcSO.moveSpeed;
        }

        public void StopImmediatly()
        {
            _rigid.linearVelocityX = 0;
        }

        public void Flip()
        {
            npcRenderer.Flip();
            LookDir *= -1;
        }
    }
}
