namespace Engine
{
    public interface IPoolObject
    {
        bool IsPushed { get; set; }

        void Create();

        void OnPush();
        
        void FailedPush();
    }
}