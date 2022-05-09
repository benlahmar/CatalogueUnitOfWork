using Catalogue.Core.Interfaces;
using Catalogue.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _ApplicationDbContext;
        public UnitOfWork(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext=ApplicationDbContext;
            categorieRepository = new BaseRepository<Categorie>(_ApplicationDbContext);
            productRepository= new BaseRepository<Produit>(_ApplicationDbContext);
        }

        public IBaseRepository<Categorie> categorieRepository { get; private set; }

        public IBaseRepository<Produit> productRepository { get; private set; }

        public int complete()
        {
            return _ApplicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _ApplicationDbContext.Dispose();
        }
    }
}
