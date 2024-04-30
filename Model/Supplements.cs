namespace Model;

public class Supplements : Product
{
    public string Creatine { get; set; }
    public string ProteinPowder { get; set; }
    public string ProteinBar { get; set; }

    public override string ToString()
    {
        return $"Creatine: {Creatine}, Protein Powder: {ProteinPowder}, Protein Bar: {ProteinBar}, {base.ToString()}";
    }
}