using UnityEngine;

namespace Core.StageController
{
    [CreateAssetMenu(menuName = "SO/Stage/Ingame")]
    public class IngameSO : StageSO
    {
        [TextArea(5, 20)]
        public string stageExplain;
        //���� ���� ����?
    }
}
