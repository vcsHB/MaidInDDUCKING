using Basement.Mission;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MissionSelectButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _explainText;
    private MissionSO _mission;

    public void Init(MissionSO mission)
    {
        _mission = mission;
        _nameText.SetText(_mission.missionName);
        _explainText.SetText(_mission.missionExplain);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�̰͵� �� �� �ε� �׷��ɷ� �ٲٰ� �׷�����?
        SceneManager.LoadScene(_mission.sceneName);
    }
}
