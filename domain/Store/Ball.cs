namespace Store;
public class Ball
{
    public int ID { get; }

    public string Name { get; }

    public string Brand {  get; }

    public string Model { get; }


    public Ball(int id, string name, string brand, string model)
    {
        ID = id;
        Name = name;
        Brand = brand;
        Model = model;
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
