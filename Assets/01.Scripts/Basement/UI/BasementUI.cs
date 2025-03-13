using System;
using System.Collections.Generic;
using UI;
using UnityEditor.Compilation;
using UnityEngine;

namespace Basement
{
    public abstract class BasementUI : MonoBehaviour, IWindowPanel
    {
        public Action onCompleteOpen;
        public Action onCompleteClose;

        // ���� ������, ���� ���� UI
        public List<BasementUI> connectedUIList = new List<BasementUI>();
        // ����� UI��: linkedUI�� ���������� �� UI�� ���� linkedUI�� ����
        public BasementUI linkedUI;
        // �ݴ� UI: �� UI���� ���� ����
        public BasementUI oppositeUI;

        //���ÿ� ���� ����
        private bool _isOpenCloseByOpposite = false;
        [SerializeField] protected bool _openWithConnectedUI;
        [SerializeField] protected bool _closeWithConnectedUI;
        [SerializeField] protected bool _openAfterConnectedUICloseAnim;
        [SerializeField] protected bool _closeAfterConnectedUICloseAnim;
        [SerializeField] protected bool _openAfterLinkedUICloseAnim;
        [SerializeField] protected bool _closeAfterLinkedUICloseAnim;

        #region Property
        public bool OpenWithConnectedUI => _openWithConnectedUI;
        public bool CloseWithConnectedUI => _closeWithConnectedUI;
        public bool OpenAfterConnectedUICloseAnim => _openAfterConnectedUICloseAnim;
        public bool CloseAfterConnectedUICloseAnim => _closeAfterConnectedUICloseAnim;
        public bool OpenAfterLinkedUICloseAnim => _openAfterLinkedUICloseAnim;
        public bool CloseAfterLinkedUICloseAnim => _closeAfterLinkedUICloseAnim;
        public bool isOpend { get; private set; } = false;

        #endregion

        protected abstract void OpenAnimation();
        protected abstract void CloseAnimation();

        public virtual void Open()
        {
            if (connectedUIList != null)
            {
                connectedUIList.ForEach(ui =>
                {
                    if (ui.OpenAfterConnectedUICloseAnim) onCompleteOpen += ui.Open;
                    else if (ui.OpenWithConnectedUI) ui.Open();
                });
            }
            if (oppositeUI != null)
            {
                if (_isOpenCloseByOpposite)
                {
                    _isOpenCloseByOpposite = false;
                }
                else
                {
                    oppositeUI.OpenCloseByOpposite();
                    oppositeUI.CloseAllUI();
                }
            }
            isOpend = true;
            OpenAnimation();
        }

        public virtual void Close()
        {
            if (linkedUI != null && linkedUI.isOpend)
            {
                if (linkedUI.CloseAfterLinkedUICloseAnim) onCompleteClose += linkedUI.Close;
                else linkedUI.Close();

                return;
            }
            if (connectedUIList != null)
            {
                connectedUIList.ForEach(ui =>
                {
                    if (ui.CloseAfterConnectedUICloseAnim) onCompleteClose += ui.Close;
                    else if (ui.CloseWithConnectedUI) ui.Close();
                });
            }
            if (oppositeUI != null)
            {
                if (_isOpenCloseByOpposite)
                {
                    _isOpenCloseByOpposite = false;
                }
                else
                {
                    oppositeUI.OpenCloseByOpposite();
                    oppositeUI.Open();
                }
            }
            isOpend = false;
            CloseAnimation();
        }

        public virtual void CloseAllUI()
        {
            if (linkedUI != null && linkedUI.isOpend) linkedUI.Close();
            if (connectedUIList != null) connectedUIList.ForEach(ui => ui.Close());
            if (oppositeUI != null)
            {
                if (_isOpenCloseByOpposite)
                {
                    _isOpenCloseByOpposite = false;
                }
                else
                {
                    oppositeUI.OpenCloseByOpposite();
                    oppositeUI.Open();
                }
            }
            isOpend = false;
            CloseAnimation();
        }

        public virtual void SetOppositeUI(BasementUI basementUI)
        {
            if (basementUI == null)
            {
                Debug.LogError("BasementUI you want to set opposite is null\nIf you want to remove oppositeUI use RemovOppositeUI function");
                return;
            }

            if (isOpend && basementUI.isOpend) basementUI.Close();
            if (!isOpend && !basementUI.isOpend) Open();

            oppositeUI = basementUI;
            if (basementUI.oppositeUI != this) basementUI.SetOppositeUI(this);
        }

        public void RemoveOppositeUI(bool closeOpositeUI = false)
        {
            if (oppositeUI == null) return;

            if (closeOpositeUI) oppositeUI.Close();
            oppositeUI.oppositeUI = null;
            oppositeUI = null;
        }

        public virtual void SetUILink(BasementUI basementUI)
        {
            if (basementUI == null)
            {
                Debug.LogError("BasementUI you want to link is null\nIf you want to remove oppositeUI use RemovLinkedUI function");
                return;
            }

            if (linkedUI != null) linkedUI.Close();
            linkedUI = basementUI;
        }

        public void RemoveLinkedUI(bool closeLinkedUI = false)
        {
            if (oppositeUI == null)
            {
                Debug.LogWarning("There is no linkedUI but you still trying to remove it");
                return;
            }

            if (closeLinkedUI) linkedUI.Close();
            linkedUI = null;
        }

        private void OpenCloseByOpposite() => _isOpenCloseByOpposite = true;
    }
}
