namespace Mt.Core.Infrastructure
{

    /// <summary>
    /// 集成此接口的都会在app_start事件调用execute
    /// </summary>
    public interface IStartupTask 
    {
        void Execute();

        int Order { get; }
    }
}
