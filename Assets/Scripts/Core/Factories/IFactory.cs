namespace Factory
{
    public interface IFactory<out T>
    {
        T CreateInstance(params object[] args);
        void RegisterTypes();
    }
}