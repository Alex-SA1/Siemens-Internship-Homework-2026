namespace Problem2.domain.discount;

public class ThresholdDiscountPolicy : IDiscountPolicy
{
    private readonly decimal _threshold;
    private readonly decimal _percentage;

    public ThresholdDiscountPolicy(decimal threshold, decimal percentage)
    {
        _threshold = threshold;
        _percentage = percentage;
    }

    public decimal Apply(decimal amount)
    {
        if (amount > _threshold)
            return amount * (1 - _percentage / 100m);

        return amount;
    } 
}