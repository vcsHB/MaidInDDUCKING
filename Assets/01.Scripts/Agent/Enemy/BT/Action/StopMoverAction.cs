using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "StopMover", story: "stop [mover] with [YAxis] ", category: "Action", id: "9e2c488757cb46b4c995018575016e4c")]
    public partial class StopMoverAction : Action
    {
        [SerializeReference] public BlackboardVariable<AgentMovement> Mover;
        [SerializeReference] public BlackboardVariable<bool> YAxis;

        protected override Status OnStart()
        {
            if(Mover.Value == null) Debug.Log("걍 Mover가 null");
            Mover.Value.StopImmediately(YAxis.Value);
            return Status.Success;
        }

    }


}