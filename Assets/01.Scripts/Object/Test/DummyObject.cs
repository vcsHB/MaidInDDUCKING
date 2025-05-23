using Combat;
using UnityEngine;
namespace ObjectManage
{

    public class DummyObject : MonoBehaviour
    {
        public Transform GetTransform => transform;
        private Rigidbody2D _rigid;
        private float _defaultGravity;

        private void Awake() {
            _rigid = GetComponent<Rigidbody2D>();
            _defaultGravity = _rigid.gravityScale;
        }

        // public void Grab()
        // {
        //     _rigid.gravityScale = 0.1f;
        // }

        // public void Release()
        // {
        //     _rigid.gravityScale = _defaultGravity;
        // }
    }
}