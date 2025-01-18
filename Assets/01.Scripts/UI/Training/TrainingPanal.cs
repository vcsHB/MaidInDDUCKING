using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TrainingPanal : MonoBehaviour, IUIPanel
{
    public Dictionary<CharacterType, int> characterHealth;

    [SerializeField] private CharacterSelectPanel _selectPanel;
    private Training[] trainings;
    private float _easingTime = 0.2f;

    public RectTransform RectTrm => transform as RectTransform;

    private void Awake()
    {
        InitializeCharacterHealth();
        trainings = GetComponentsInChildren<Training>();

        foreach (Training training in trainings)
        {
            training.Init(_selectPanel);
        }
    }

    public void InitializeCharacterHealth()
    {
        characterHealth = new Dictionary<CharacterType, int>();
        //���߿� �̰� ���õȰ� ��Ȯ�� ��ȹ�� �Ǹ� �׶� ����, �ҷ����� �����ŵ� ������

        int healthInitailizeValue = 7;
        characterHealth.Add(CharacterType.Katana, healthInitailizeValue);
        characterHealth.Add(CharacterType.CrescentBlade, healthInitailizeValue);
        characterHealth.Add(CharacterType.Cross, healthInitailizeValue);
    }

    public int GetCharacterHealt(CharacterType character)
        => characterHealth[character];

    public bool TryUseCharacterHealth(CharacterType character, int value)
    {
        if (characterHealth[character] < value) return false;

        characterHealth[character] -= value;
        return true;
    }

    public void Open(Vector2 position)
    {
        RectTrm.DOAnchorPosY(0f, _easingTime);
    }

    public void Close()
    {
        RectTrm.DOAnchorPosY(-1920f, _easingTime);
    }
}
