using System.Linq;
using UnityEngine;
using Basement.NPC;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Basement
{
    public class Cafe : BasementRoom
    {
        private List<Table> _tableList;

        private Queue<Employee> _employeeQueue;

        //���� ���� ������ ��
        private Queue<Customer> _lineUpCustomers;
        // ���̺� ���� ����
        private Queue<Customer> _menuWaitingCustomers;

        public bool isCafeOpen = false;
        public BasementTime cafeOpenTime;
        public Furniture counterFurniture;

        public Transform employeeParent;
        public Transform customerParent;
        public Transform employeePosition;
        public Transform exit;
        public CharacterEnum PositionedCharacter;

        //������
        [SerializeField] private CustomerSO debugCustomer;
        [SerializeField] private CafeNPC npc;
        [SerializeField] private List<EmployeeSO> employeeList;

        private CafeUI _cafeUI;
        public CafeUI CafeUI
        {
            get
            {
                if (_cafeUI == null)
                    _cafeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Cafe) as CafeUI;
                return _cafeUI;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            npc.Init(this);
            _lineUpCustomers = new Queue<Customer>();
            _menuWaitingCustomers = new Queue<Customer>();
            _employeeQueue = new Queue<Employee>();

            _tableList = GetComponentsInChildren<Table>().ToList();

            employeeList.ForEach(employee =>
            {
                Employee employeeInstance = Instantiate(employee.employeePf, employeeParent);
                employeeInstance.gameObject.SetActive(false);
                _employeeQueue.Enqueue(employeeInstance);
            });
        }

        private void OnDisable()
        {
            counterFurniture.InteractAction -= OpenCafeUI;
        }

        private void Update()
        {
            //FOR DEBUGING
            if (Keyboard.current.cKey.wasPressedThisFrame)
                AddCustomer(debugCustomer);

            if(Keyboard.current.vKey.wasPressedThisFrame)
            {
                Debug.Log($"���� ��: {_employeeQueue.Count} �� ��{_menuWaitingCustomers.Count}");
            }
        }

        /// <summary>
        /// �����Ϸ� �����̱� ����
        /// </summary>
        private void TryServeMenu()
        {
            while(_menuWaitingCustomers.TryPeek(out Customer customer))
            {
                if (_employeeQueue.TryDequeue(out Employee employee))
                {
                    //���� �⵿
                    employee.transform.position = employeePosition.position;
                    employee.Init(this, customer.TargetTable);
                    _menuWaitingCustomers.Dequeue();
                }
                else break;
            }

            //���� ������ �ְ�
            //if (_employeeQueue.TryDequeue(out Employee employee))
            //{
            //    //�޴��� ��ٸ��� ���� �ִٸ�
            //    if (_menuWaitingCustomers.TryDequeue(out Customer customer))
            //    {
            //        Debug.Log(_menuWaitingCustomers.Count);
            //        employee.transform.position = employeePosition.position;
            //        employee.Init(this, customer.TargetTable);
            //    }
            //}
        }

        private Table FindEmptyTable()
            => _tableList.Find(table => table.IsCustomerExsist() == false);

        private void AddCustomer(CustomerSO customerSO)
        {
            Customer customer = Instantiate(customerSO.customerPf, customerParent);
            EnterCustomer(customer);
        }

        private void EnterCustomer(Customer customer)
        {
            Table emptyTable = FindEmptyTable();

            if (emptyTable != null)
            {
                customer.gameObject.SetActive(true);
                customer.SetTable(emptyTable);
                customer.Init(this);
                customer.transform.position = exit.position;
            }
            else
            {
                //�ڸ��� ������ ������ �󿡼��� ���� �����
                customer.gameObject.SetActive(false);
                _lineUpCustomers.Enqueue(customer);
            }
        }

        /// <summary>
        /// �մ��� ���̺� �ɾ��� ��
        /// </summary>
        /// <param name="customer"></param>
        public void OnCustomerSitTable(Customer customer)
        {
            _menuWaitingCustomers.Enqueue(customer);
            TryServeMenu();
        }

        /// <summary>
        /// �մ��� ���̺��� ���� ��
        /// </summary>
        /// <param name="employee"></param>
        public void ReturnEmployee(Employee employee)
        {
            employee.gameObject.SetActive(false);
            _employeeQueue.Enqueue(employee);
        }

        // �մ��� ������ ���̺��� ����ٴ� ��������
        // �ٽ� ������ �ֵ��� �ִ��� Ȯ��
        public void OnLeaveCustomer(Customer customer)
        {
            TryServeMenu();
        }



        public void PassTime(int time)
        {
            //if (isCafeOpen == false) return;

            //�ϴ� 30�� ���� 30% Ȯ���� �����ϴ°ɷ�?
            //bool isCustomerEnter = false;
            //int totalCustomer = 0;
            //int totalCosts = 0;
            //int totalTips = 0;

            //for (int i = 0; i < time / 30; i++)
            //{
            //    if (Random.Range(0, 100) < 30)
            //    {
            //        isCustomerEnter = true;

            //        int cost = Random.Range(2, 6);
            //        int tip = Random.Range(0, (int)(cost * 0.3f));

            //        totalCustomer++;
            //        totalCosts += cost;
            //        totalTips += tip;
            //    }
            //}
            //if (isCustomerEnter == false) return;

            //string text = $"{totalCustomer}�� �湮\n����: {totalCosts}{(totalTips > 0 ? $"+TIP{totalTips}" : "")}";
            //UIManager.Instance.msgText.PopMSGText(PositionedCharacter, text);
            ////��ȭ �߰����ֱ�
        }

        private void OpenCafeUI()
        {
            CafeUI.Init(this);
            CafeUI.Open();
            //UIManager.Instance.returnButton.ChangeReturnAction(CafeUI.Close);
            UIManager.Instance.basementUI.Close();
            RoomUI.Close();
        }

        public override void CloseUI()
        {
            CafeUI.Close();
        }

        public override void Init(BasementController basement)
        {
            base.Init(basement);
            counterFurniture.Init(this);
            counterFurniture.InteractAction += OpenCafeUI;
        }
    }
}
