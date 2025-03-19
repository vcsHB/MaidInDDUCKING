using TMPro;

namespace Dialog
{
    public abstract class TagAnimation
    {
        protected TMP_TextInfo _txtInfo;

        protected AnimTiming _timing;
        protected string _param;        //�Ķ����
        protected bool _checkEndPos;    //���� Ȯ���ϴ��� </�� ã�ƾ��ϴ��� Ȯ���ϴ°���
        protected bool _endAnimating;   //Ÿ�̹��� Start�� End�� �� ���� �ٲ㼭 Ȯ�Τ�
        protected bool _stopReadingDuringAnimation = false;
        protected bool _animationComplete = false;

        public TagEnum tagType;
        public int animStartPos;
        public int animLength;

        public AnimTiming Timing => _timing;
        public string Param => _param;
        public bool EndAnimating => _endAnimating;
        public bool CheckEndPos => _checkEndPos;
        public bool StopReadingDuringAnimation => _stopReadingDuringAnimation;

        public void SetParameter(string param) => _param = param;

        public abstract void Play();
        public abstract void Complete();
        public abstract bool SetParameter();

        public virtual void Init()
        {
            _endAnimating = false;
            _animationComplete = false;
        }

        public virtual void SetTextInfo(TMP_TextInfo txtInfo)
        {
            _endAnimating = false;
            _txtInfo = txtInfo;
        }
    }

    public enum AnimTiming
    {
        //Start�� Update�� �Ѵ� LateUpdate���� �������൵ ��
        //�ٵ� End�� ���� �� while���ȿ� ��Ƶΰ� �ؾ���
        //OnTextOut�� �ؽ�Ʈ ����� �� While���� ��Ƶΰ�
        Start,
        Update,
        OnTextOut,
        End
    }
}
