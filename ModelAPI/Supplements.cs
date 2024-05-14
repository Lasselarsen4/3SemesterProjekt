using System;

namespace ModelAPI
{
    public class Supplements : Product
    {
        public Supplements(int productId, string productName, decimal productPrice, string productDescription,
            string creatine, string proteinPowder, string proteinBar) 
            : base(productId, productName, productPrice, productDescription)
        {
            Creatine = creatine;
            ProteinPowder = proteinPowder;
            ProteinBar = proteinBar;
        }

        public string Creatine { get; set; }
        public string ProteinPowder { get; set; }
        public string ProteinBar { get; set; }

        public override string ToString()
        {
            return $"Creatine: {Creatine}, Protein Powder: {ProteinPowder}, Protein Bar: {ProteinBar}, {base.ToString()}";
        }
    }
}