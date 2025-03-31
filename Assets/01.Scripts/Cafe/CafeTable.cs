using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class CafeTable : MonoBehaviour
    {
        public Transform customerPosition;

        public ProcessInputObject clickProcess;
        public Image iconRenderer;
        public Image patientFill;
        public Sprite serveIcon, cleanIcon;

        private CafePlayer _player;
        private Collider2D _collider;
        private bool _isCustomerWaitingMenu = false;
        private bool _isAddInput = false;
        private float _currentWaitingTime;
        private float _customerPatientTime;

        public CafeCustomer AssingedCustomer { get; private set; }
        public bool IsClean { get; private set; } = true;
        public bool IsCustomerExsist { get => AssingedCustomer != null; }
        public float WaitingTime => _currentWaitingTime;

        private void Awake()
        {
            IsClean = true;
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (_isCustomerWaitingMenu)
            {
                _currentWaitingTime += Time.deltaTime;
                patientFill.fillAmount = _currentWaitingTime / _customerPatientTime;

                if (_currentWaitingTime >= _customerPatientTime)
                {
                    //Debug.Log("�ֹ������� ������ �ȳ���");
                }
            }
        }

        //�����ͻ� �մ� �Ҵ�
        public void SetCustomer(CafeCustomer customer)
        {
            AssingedCustomer = customer;
            _customerPatientTime = customer.customerSO.menuWaitingTime;
        }

        //���ӿ��� �մ� �Ҵ�
        public void CustomerSit()
        {
            _collider.enabled = true;
            iconRenderer.gameObject.SetActive(true);
            iconRenderer.sprite = serveIcon;

            _isCustomerWaitingMenu = true;
            _currentWaitingTime = 0;
        }

        //�޴��� ������ �� ��
        public void OnServingMenu()
        {
            _collider.enabled = false;
            iconRenderer.gameObject.SetActive(false);
            AssingedCustomer.GetFood();
            _isCustomerWaitingMenu = false;
            patientFill.fillAmount = 0;
            _player.ServeFood();
            _isAddInput = false;
        }

        //�մ��� ������
        public void LeaveCustomer()
        {
            if (AssingedCustomer == null) return;

            AssingedCustomer = null;
            IsClean = false;

            _collider.enabled = true;
            iconRenderer.gameObject.SetActive(true);
            iconRenderer.sprite = cleanIcon;
            patientFill.fillAmount = 0;
        }


        public void CleanTable()
        {
            _collider.enabled = false;
            iconRenderer.gameObject.SetActive(false);
            IsClean = true;
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _player) == false) return;

            if (_isCustomerWaitingMenu)
            {
                if (_player.isGetFood == false) return;

                _isAddInput = true;
                _player.AddInteract(OnServingMenu);
                return;
            }

            if (IsClean || _player.isGetFood) return;
            clickProcess.Init(this);
        }


        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out _player)) return;

            Debug.Log(_isAddInput);
            if (_isAddInput)
            {
                _player.RemoveInteract(OnServingMenu);
                _isAddInput = false;
            }

            clickProcess.Close();
        }


        public bool CanCustomerSitdown() => (IsClean && !IsCustomerExsist);
    }
}
