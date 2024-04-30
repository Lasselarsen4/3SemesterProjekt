namespace Model;

public class Equipment : Product
{
    public string Weights { get; set; }
    public string Benches { get; set; }
    public string Handles { get; set; }

    public override string ToString()
    {
        return $"Weights: {Weights}, Benches: {Benches}, Handles: {Handles}, {base.ToString()}";
    }
}