namespace _Game.Source.Domain
{
    public interface IValidator<TContext>
    {
        bool IsValid(TContext context);
    }
}