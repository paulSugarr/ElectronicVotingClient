using UnityEngine;

namespace Networking.Commands
{
    public class CommandTest : MonoBehaviour
    {
        public void TestCommand()
        {
            var command = new LogCommand("logg");
            var json = fastJSON.JSON.ToJSON(command.GetInfo());
            var nextCommand = new LogCommand(json);
            nextCommand.Execute();
        }
    }
}