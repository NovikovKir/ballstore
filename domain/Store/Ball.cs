namespace Store;
public class Ball
{
    public int Id { get; }

    public string Name { get; }

    public string Brand {  get; }

    public string Model { get; }

    public string Description { get; }

    public decimal Price { get; }


    public Ball(int id, string name, string brand, string model, string description, decimal price)
    {
        Id = id;
        Name = name;
        Brand = brand;
        Model = model;
        Description = description;
        Price = price;
    }

    static public bool IsBrandOrModel(string s)
    {
        if (s == null) return false;

        s = s.Replace("-", "")
            .Replace(" ", "")
            .ToUpper();

        return true;
    }
}
