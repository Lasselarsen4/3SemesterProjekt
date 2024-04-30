namespace Model;

public class Accessories : Product
{
    public string Straps { get; set; }
    public string Clothes { get; set; }
    public string Gloves { get; set; }

    public override string ToString()
    {
        return $"Straps: {Straps}, Clothes: {Clothes}, Gloves: {Gloves}, {base.ToString()}";
    }
}