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
        Task<List<Furniture>> GetAvailableFurniture();
        Task<List<Furniture>> GetAllFurniture();
        Task<FurnitureLike> AddLike(FurnitureLikeForm furnitureLikeForm);
        Task<bool> RemoveLike(int furnitureLikeId);

        Task<bool> MarkAsSold(SaleForm saleForm);
    }

    public class FurnitureService : IFurnitureService
    {
        private MyContext _context;

        public FurnitureService(MyContext context)
        {
            _context = context;
        }

        public async Task<List<Furniture>> GetAllFurniture()
        {
            await Task.Delay(0);
            List<Furniture> allFurniture = _context.Furniture
                        .Include(f => f.sale)
                        .Include(f => f.type)
                        .Include(f => f.images)
                        .ThenInclude(i => i.s3Image)
                        .Include(f => f.likedByUsers)
                        .ToList();

            return allFurniture;
        }

        public async Task<List<Furniture>> GetAvailableFurniture()
        {
            await Task.Delay(0);
            List<Furniture> allFurniture = _context.Furniture
                        .Include(f => f.sale)
                        .Include(f => f.type)
                        .Include(f => f.images)
                        .ThenInclude(i => i.s3Image)
                        .Include(f => f.likedByUsers)
                        .Where(f => f.sale == null)
                        .ToList();

            return allFurniture;
        }

        public async Task<FurnitureLike> AddLike(FurnitureLikeForm furnitureLikeForm)
        {
            try
            {
                await Task.Delay(0);
                if(_context.FurnitureLikes.Any(l => l.userId == furnitureLikeForm.userId && l.furnitureId == furnitureLikeForm.furnitureId))
                {
                    return null;
                }
                FurnitureLike toAdd =new FurnitureLike(furnitureLikeForm);
                _context.Add(toAdd);
                _context.SaveChanges();
                return toAdd;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RemoveLike(int furnitureLikeid)
        {
            try
            {
                await Task.Delay(0);
                FurnitureLike toRemove = _context.FurnitureLikes.FirstOrDefault(f => f.furnitureLikeId == furnitureLikeid);
                if(toRemove == null)
                {
                    return false;
                }

                _context.Remove(toRemove);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> MarkAsSold(SaleForm saleForm)
        {
            await Task.Delay(0);
            try
            {
                _context.Add(new Sale(saleForm));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}