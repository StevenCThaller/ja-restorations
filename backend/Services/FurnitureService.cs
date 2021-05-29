using System;
using System.Collections.Generic;
using backend.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Services
{
    public interface IFurnitureService
    {
        List<Furniture> GetAvailableFurniture();
    }

    public class FurnitureService : IFurnitureService
    {
        private MyContext _context;

        public FurnitureService(MyContext context)
        {
            _context = context;
        }

        public List<Furniture> GetAvailableFurniture()
        {
            List<Furniture> allFurniture = _context.Furniture
                        .Include(f => f.sale)
                        .Include(f => f.images)
                        .ThenInclude(i => i.s3Image)
                        .ToList();

            return allFurniture.Where(f => f.sold == false).ToList();
        }
    }
}