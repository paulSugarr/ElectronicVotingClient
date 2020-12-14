using Networking;
using UnityEngine;

namespace Core
{
    public class StartReferences : MonoBehaviour
    {
        [SerializeField] private NetworkManager _networkManager;

        public NetworkManager NetworkManager => _networkManager;
        public string MainConfigPath => "Assets/Descriptions/main_config.json";
    }
}