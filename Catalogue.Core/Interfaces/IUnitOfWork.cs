using Catalogue.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Categorie> categorieRepository { get; }
        IBaseRepository<Produit> productRepository { get; }
        int complete();
    }
}
