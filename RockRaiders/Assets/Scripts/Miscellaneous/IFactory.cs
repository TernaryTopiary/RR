namespace Assets.Scripts.Miscellaneous
{
    public interface IFactory<out T>
    {
        T New();
    }
}