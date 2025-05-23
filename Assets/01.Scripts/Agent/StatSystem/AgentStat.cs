using System;
using System.Linq;
using StatSystem;
using UnityEngine;

namespace Agents
{

    [Obsolete("Don't use this class in project, use \'AgentStatus\' class", true)]
    public class AgentStat : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private StatOverride[] _statOverrides;
        private StatSO[] _stats;
        private Agent _owner;

        public void Initialize(Agent agent)
        {
            _owner = agent;
            _stats = _statOverrides.Select(x => x.CreateStat()).ToArray();
        }

        public StatSO GetStat(StatSO stat)
        {
            Debug.Assert(stat != null, "Stat : GetStat - stat cannot be null");
            return _stats.FirstOrDefault(x => x.statName == stat.statName);
        }

        public bool TryGetStat(StatSO stat, out StatSO outStat)
        {
            Debug.Assert(stat != null, "TryGetStat : GetStat - stat cannot be null");
            outStat = _stats.FirstOrDefault(x => x.statName == stat.statName);
            return outStat != null;
        }

        public void SetBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue = value;

        public float GetBaseValue(StatSO stat)
            => GetStat(stat).BaseValue;

        public float IncreaseBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue += value;


        public void AddModifier(StatSO stat, object key, float value)
            => GetStat(stat).AddBuffDebuff(key, value);


        public void RemoveModifier(StatSO stat, object key)
            => GetStat(stat).RemovedBuffDebuff(key);

        public void ClearAllModifiers()
        {
            foreach (StatSO stat in _stats)
            {
                stat.ClearBuffDebuff();
            }
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }
    }
}
