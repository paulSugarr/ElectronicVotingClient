using System.Collections.Generic;
using UI;

namespace Networking.Commands
{
    public class LoginAcceptanceCommand : ICommand
    {
        public string Type { get; }
        public bool Allow { get; }

        public LoginAcceptanceCommand(bool allow)
        {
            Type = "login_accept";
            Allow = allow;
        }
        public LoginAcceptanceCommand(Dictionary<string, object> info)
        {
            Type = "login_accept";
            Allow = (bool) info["allow"];
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("allow", Allow);
            return result;
        }

        public void Execute()
        {
            UIController.Instance.Loading = false;
            UIController.Instance.MainContent = Allow;
            UIController.Instance.DeclineLoginWindow = !Allow;
        }
    }
}