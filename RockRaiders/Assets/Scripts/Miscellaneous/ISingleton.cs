namespace Assets.Scripts.Miscellaneous
{
    public interface ISingleton
    {

    }

    public abstract class Singleton<T> : ISingleton
    where T : Singleton<T>, new()
    {
        private static volatile T _instance;

        public static T GetInstance()
        {
            var lockingObject = new object();
            if (_instance != null) return _instance;
            lock (lockingObject) if (_instance == null) _instance = new T();
            return _instance;
        }

        public void OverrideInstance(T instance)
        {
            _instance = instance;
        }
    }
}