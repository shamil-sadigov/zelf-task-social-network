using Domain.ValueObjects;

namespace Domain.Contracts
{
    public interface IPopularityEvaluator
    {
        ClientPopularity Evaluate();
    }
}