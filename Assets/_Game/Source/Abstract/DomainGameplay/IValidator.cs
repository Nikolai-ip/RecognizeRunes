namespace _Game.Source.Abstract.DomainGameplay
{
    public interface IValidator<TContext>
    {
        bool IsValid(TContext context);
    }
}