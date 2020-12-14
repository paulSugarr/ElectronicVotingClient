namespace Networking.Commands
{
    public interface ICommand
    {
        string Type { get; }
        void Execute();
    }
}