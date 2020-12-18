using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneRestarter : MonoBehaviour
    {
        public void RestartScene()
        {
            UIController.Instance.Loading = false;
            UIController.Instance.MainContent = false;
        }
    }
}