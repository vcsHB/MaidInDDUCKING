using System;
using System.Collections.Generic;
using Agents.Animate;
using Agents.Players;
using UnityEngine;
namespace Office.CharacterControl
{
    public class OfficePlayerStateMachine
    {
        protected Dictionary<string, OfficePlayerState> _stateDictionary = new();
        public OfficePlayerState CurrentState => _currentState;
        private OfficePlayerState _currentState;
        private OfficePlayer _player;
        private OfficePlayerRenderer _renderer;

        public OfficePlayerStateMachine(OfficePlayer player)
        {
            _player = player;
            _renderer=  player.GetCompo<OfficePlayerRenderer>();
        }


        public void InitializeState()
        {
            AddState("Idle", _renderer.IdleParam);
            AddState("Move", _renderer.MoveParam);

        }

        private void AddState(string stateName, AnimParamSO param)
        {
             Type t = Type.GetType($"Agents.Players.FSM.{stateName}State");
            OfficePlayerState state = Activator.CreateInstance(t, _player, this, param) as OfficePlayerState;
            _stateDictionary.Add(stateName, state);
        }

        public void UpdateState()
        {
            _currentState.Update();
        }
        public void ChangeState(OfficePlayerState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();

        }
    }
}