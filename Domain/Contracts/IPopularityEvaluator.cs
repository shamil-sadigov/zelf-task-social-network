#region

using Domain.ValueObjects;

#endregion

namespace Domain.Contracts
{
    public interface IPopularityEvaluator
    {
        ClientPopularity Evaluate();
    }
}