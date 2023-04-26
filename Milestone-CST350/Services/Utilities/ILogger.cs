namespace Activity2_2.Services.Utilities
{
    public interface ILogger
    {
        ILogger GetInstance();
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
    }
}
