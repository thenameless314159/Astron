namespace Astron.Expressions.Builder
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}
