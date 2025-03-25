using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(fileName = "WaveSO", menuName = "SO/Cafe/WaveSO")]
    public class CustomerWaveSO : ScriptableObject
    {
        [Tooltip("��ȯ�� �մ��� ����")]
        public List<CafeCustomerSO> customerToSpawn;

        [Tooltip("�մ��� �󸶳� ��ȯ����(x �� y �� ����)")]
        public int spawnValue;

        [Tooltip("�մ� ���� ����(x �� y �� ����)")]
        public int spawnDelay;

        [Tooltip("���� ���̺갡 ������ �󸶳� �ڿ� ����� ����")]
        public float waveDelay;
    }
}
