using Basement.Training;
using UnityEngine;

namespace Basement
{
    public class Cafe : BasementRoom
    {
        private CharacterEnum _positionedCharacter;
        private CafeUI _cafeUI;

        public bool isCafeOpen = false;
        public BasementTime cafeOpenTime;
        public Furniture counterFurniture;

        public CharacterEnum PositionedCharacter
        {
            get
            {
                return _positionedCharacter;
            }
            set
            {
                _positionedCharacter = value;
                profitRange
                    = CharacterManager.Instance.GetCharacterExpectationProfit(_positionedCharacter);
            }
        }

        public Vector2 profitRange;
        public int totalProfit = 0;

        protected override void Start()
        {
            counterFurniture.Init(this);
            counterFurniture.InteractAction += OpenCafeUI;
            _cafeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Cafe) as CafeUI;
            Init(BasementController);
        }

        private void OnDisable()
        {
            counterFurniture.InteractAction -= OpenCafeUI;
        }

        private void OpenCafeUI()
        {
            _cafeUI.Init(this);
            _cafeUI.Open();
        }

        public void PassTime(int time)
        {
            if (isCafeOpen == false) return;

            //�ϴ� 30�� ���� 30% Ȯ���� �����ϴ°ɷ�?
            bool isCustomerEnter = false;
            int totalCustomer = 0;
            int totalCosts = 0;
            int totalTips = 0;

            for (int i = 0; i < time / 30; i++)
            {
                if (Random.Range(0, 100) < 30)
                {
                    isCustomerEnter = true;

                    int cost = Random.Range(2, 6);
                    int tip = Random.Range(0, (int)(cost * 0.3f));

                    totalCustomer++;
                    totalCosts += cost;
                    totalTips += tip;
                }
            }
            if (isCustomerEnter == false) return;

            string text = $"{totalCustomer}�� �湮\n����: {totalCosts}{(totalTips > 0? $"+TIP{totalTips}" : "")}";
            UIManager.Instance.msgText.PopMSGText(PositionedCharacter, text);
            //��ȭ �߰����ֱ�
        }

        protected override void CloseUI()
        {
            _cafeUI.Close();
        }
    }
}
