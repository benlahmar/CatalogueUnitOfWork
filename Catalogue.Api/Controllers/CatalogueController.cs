using Catalogue.Core.Interfaces;
using Catalogue.Core.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        //public readonly IBaseRepository<Categorie> _repCategorie;
        //public readonly IBaseRepository<Produit> _repProduit;
        
        private readonly IUnitOfWork _unitOfWork;
        public CatalogueController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            Categorie c = _unitOfWork.categorieRepository.findById(id);
            _unitOfWork.categorieRepository
                .FindByCondition(x=> x.Id==id)
                .Include(o=>o.Produits)
                .First();
            if(c==null)
                return NoContent();
            else
            return Ok(c);
        }

        [HttpGet()]
        public IActionResult all(int id)
        {
            return Ok(_unitOfWork.categorieRepository.findAll());
        }

        [HttpPost]
        public IActionResult save(Categorie c)
        { 
            _unitOfWork.categorieRepository.add(c);
            _unitOfWork.complete();
            return Ok(c);
        }

        [HttpPost("{id}/produits")]
        public IActionResult saveproduit(Produit p, int id)
        {
            var categorie = _unitOfWork.categorieRepository.findById(id);
            if (categorie == null)
                return BadRequest();
            else
            {
                p.Categorie = categorie;
                _unitOfWork.productRepository.add(p);
                _unitOfWork.complete();
                return Ok(p);
            }
           
        }

        [HttpPost("/asyn")]
        public Task<IActionResult> saveasy(Categorie c)
        {
            Task<Categorie> cc = _unitOfWork.categorieRepository.Save(c);
            _unitOfWork.complete();

            return new Task<IActionResult>(() => Ok(cc));
        }

        [HttpGet("/produits/{id}")]
        public IActionResult getprd(int id)
        {

            Random rand = new Random();
            int r=rand.Next(10);
            if (r % 2 == 0)
               throw new Exception();
            else
            {
                Produit c = _unitOfWork.productRepository.findById(id);
                if (c == null)
                    return NoContent();
                else
                    return Ok(c);
            }
            
        }

        [HttpGet("config")]
        public String getenv()
        {
            
            return "test";
        }

    }
}
