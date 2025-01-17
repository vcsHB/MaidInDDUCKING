using System.Collections.Generic;
using UnityEngine;

public class TrainingPanal : MonoBehaviour
{
    public Dictionary<CharacterType, int> characterHealth;

    [SerializeField] private CharacterSelectPanel _selectPanel;
    private Training[] trainings;

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
}
