using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Core.models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantite { get; set; }
        public double Price { get; set; }
        public Categorie Categorie { get; set; }
    }
}
