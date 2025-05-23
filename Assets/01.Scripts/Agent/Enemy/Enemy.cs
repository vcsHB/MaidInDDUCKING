using Agents.Enemies.BT.Event;
using Combat;
using Core.EventSystem;
using System;
using Unity.Behavior;
using UnityEngine;

using Action = System.Action;

namespace Agents.Enemies
{

    public class Enemy : Agent
    {
        public Action<Enemy, string> OnDisableBody;
        [SerializeField] protected LayerMask _whatIsTarget;
        protected BehaviorGraphAgent _btAgent;
        protected string _enemyName;

        public string EnemyName => _enemyName;
        public Health HealthCompo { get; protected set; }
        public Rigidbody2D RigidCompo { get; protected set; }

        protected override void Awake()
        {
            HealthCompo = GetComponent<Health>();
            base.Awake();
            RigidCompo = GetComponent<Rigidbody2D>();
            HealthCompo.OnDieEvent.AddListener(HandleAgentDie);

            _btAgent = GetComponent<BehaviorGraphAgent>();
        }

        public virtual void Init(string name)
        {
            _enemyName = name;
        }

        

        public BlackboardVariable<T> GetVariable<T>(string variableName)
        {
            if (_btAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
            {
                return variable;
            }
            return null;
        }

        public void SetVariable<T>(string variableName, T value)
        {
            BlackboardVariable<T> variable = GetVariable<T>(variableName);
            Debug.Assert(variable != null, $"Variable {variableName} not found");
            variable.Value = value;
        }

        public Transform GetTargetInRadius(float radius)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, _whatIsTarget);
            return collider != null ? collider.transform : null;
        }
    }
}