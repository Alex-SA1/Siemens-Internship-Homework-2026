namespace Problem2.domain;

public class Customer: Entity<long>
{
    public string Name { get; }

    public Customer(string name)
    {
        Name = name;
    }
    
    
}