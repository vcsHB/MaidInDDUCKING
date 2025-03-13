using Basement.Training;
using System;
using UnityEngine;

namespace Basement.Quest
{
    [CreateAssetMenu(menuName = "SO/Basement/DailyQuest")]
    public class DailyQuestSO : ScriptableObject
    {
        public string questName;
        [TextArea] public string questexplain;

        public CharacterEnum characterType;
        public BasementRoomSO questRoom;
        public RewardStruct reward;
    }

    [Serializable]
    public struct RewardStruct
    {
        public bool isRewardVeined;
        //���߿� �ŷµ� ��� �׷��ŵ� �ְ�
        public SkillPointEnum skillPointType;
        public int point;
    }
}
