using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;
namespace UI
{
    public class UIController : MonoBehaviour
    {
        #region Singletone
        private static UIController _instance;
        public static UIController Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                {
                    _instance = FindObjectOfType<UIController>();
                    return _instance;
                }
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                return;
            }
            else
            {
                _instance = this;
            }
        }
        #endregion
        
        public bool MainContent { get; set; } = false;
        public bool VoteAcceptanceWindow { get; set; } = false;
        public bool DeclineWindow { get; set; } = false;
        public bool Loading = false;

        public static void ShowLoadingScreen(bool flag)
        {
            Instance.Loading = flag;
        }


    }
}