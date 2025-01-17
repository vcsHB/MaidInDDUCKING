using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Training : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TrainingSO training;
    public bool completeTraining = false;

    [SerializeField] private UIPopupText _popupText;
    private CharacterSelectPanel _selectPanel;
    private Transform _canvasTrm;

    public RectTransform RectTrm => transform as RectTransform;

    private void Awake()
    {
        _canvasTrm = transform.GetComponentInParent<Canvas>().transform;
    }

    public void DoTraining(CharacterType character)
    {
        if (completeTraining || training == null) return;

        completeTraining = true;

        StatType statType = training.statType;
        float percent = Random.Range(0.00f, 100.00f);

        TrainingResult trainingResult = percent <= training.greatSuccesChance ? TrainingResult.GreatSuccess :
            percent <= training.successChance ? TrainingResult.Success : TrainingResult.Fail;

        int incValue = training.increaseValue[trainingResult];
        Color textColor = training.textColor[trainingResult];


        //���⼭ ���߿� �ٷ� �˾��ؽ�Ʈ ������ �ϴ°� �ƴ϶� ĳ���� �Ʒ� �ִϸ��̼��� ������, �ִϸ��̼��� ������ �� 
        UIPopupText popupText = Instantiate(_popupText, _canvasTrm);
        popupText.SetText($"{statType.ToString()} +{incValue}", textColor, 50, 0.5f, 1, RectTrm.localPosition);

        AgentStatManager.Instance.AddStatPoint(character, statType, incValue);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _selectPanel.EnableSelectPanel(DoTraining, RectTrm.anchoredPosition);
        //DoTraining(CharacterType.Katana);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void Init(CharacterSelectPanel selectPanel)
    {
        _selectPanel = selectPanel;
    }
}
