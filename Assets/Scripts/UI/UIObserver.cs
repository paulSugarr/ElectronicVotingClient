using System;
using Core;
using Michsky.UI.ModernUIPack;
using TMPro;
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
        [SerializeField] private ModalWindowManager _declineLoginInput;
        [SerializeField] private ModalWindowManager _errorConnectionWindow;
        [SerializeField] private TMP_Text _firstCandidate;
        [SerializeField] private TMP_Text _secondCandidate;
        private UIController _uiController;
        private Context _context;

        private void Start()
        {
            _uiController = UIController.Instance;
            _context = Context.Instance;
        }

        private void Update()
        {
            _uiController = UIController.Instance;
            _loadingScreen.SetActive(_uiController.Loading);
            _mainContent.SetActive(_uiController.MainContent);
            _startContent.SetActive(!_uiController.MainContent);

            if (!_acceptWindow.isOn && _uiController.VoteAcceptanceWindow)
            {
                ShowVoteAccept();
            }
            if (!_declineWindow.isOn && _uiController.DeclineWindow)
            {
                ShowVoteDecline();
            }
            if (!_declineLoginInput.isOn && _uiController.DeclineLoginWindow)
            {
                ShowLoginDecline();
            }
            if (!_errorConnectionWindow.isOn && _uiController.ErrorConnectionWindow)
            {
                ShowErrorConnectionWindow();
            }

            _firstCandidate.text = _context.Candidates[0].ToString();
            _secondCandidate.text = _context.Candidates[1].ToString();
        }
        private void ShowVoteAccept()
        {
            _acceptWindow.OpenWindow();
            _uiController.VoteAcceptanceWindow = false;
        }
        private void ShowVoteDecline()
        {
            _declineWindow.OpenWindow();
            _uiController.DeclineWindow = false;
        }
        private void ShowLoginDecline()
        {
            _declineLoginInput.OpenWindow();
            _uiController.DeclineLoginWindow = false;
        }
        private void ShowErrorConnectionWindow()
        {
            _errorConnectionWindow.OpenWindow();
            _uiController.ErrorConnectionWindow = false;
        }
    }
}