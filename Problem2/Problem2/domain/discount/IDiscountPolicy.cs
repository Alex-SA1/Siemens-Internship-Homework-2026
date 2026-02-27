namespace Problem2.domain.discount;

public interface IDiscountPolicy
{
    /// <summary>
    /// Aplies a discount the the given amount or returns the initial amount if it is not eligibile for the discount
    /// </summary>
    /// <param name="amount">the amount to apply the discount on</param>
    /// <returns>the final amount</returns>
    decimal Apply(decimal amount);
}