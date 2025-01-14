using UnityEngine;

[CreateAssetMenu(menuName = "SO/Training")]
public class TrainingSO : ScriptableObject
{
    public string trainingName;
    public StatType statType;

    [Header("Chance is between 0 ~ 100")]
    [Space(5)]

    //�Ʒ� ���� Ȯ��
    public float successChance;
    public int successValue;
    
    [Space(15)]
    
    //�Ʒ� �뼺�� Ȯ��
    public float greatSuccesChance;
    public int greatSuccessValue;
}

public enum TrainingResult
{
    Fail,
    Success,
    GreatSuccess
}