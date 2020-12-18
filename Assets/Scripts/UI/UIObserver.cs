using Michsky.UI.ModernUIPack;
using UnityEngine;

namespace UI
{
    public class UIObserver : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private GameObject _mainContent;
        [SerializeField] private GameObject _startContent;
        [SerializeField] private ModalWindowManager _acceptWindow;
        [SerializeField] private ModalWindowManager _declineWindow;
        private void Update()
        {
            _loadingScreen.SetActive(UIController.Instance.Loading);
            _mainContent.SetActive(UIController.Instance.MainContent);
            _startContent.SetActive(!UIController.Instance.MainContent);

            if (!_acceptWindow.isOn && UIController.Instance.VoteAcceptanceWindow)
            {
                ShowVoteAccept();
            }
            if (!_declineWindow.isOn && UIController.Instance.DeclineWindow)
            {
                ShowVoteDecline();
            }
        }
        private void ShowVoteAccept()
        {
            _acceptWindow.OpenWindow();
            UIController.Instance.VoteAcceptanceWindow = false;
        }
        private void ShowVoteDecline()
        {
            _declineWindow.OpenWindow();
            UIController.Instance.DeclineWindow = false;
        }
    }
}