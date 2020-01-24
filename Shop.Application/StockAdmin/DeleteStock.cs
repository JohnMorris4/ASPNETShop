using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private readonly ApplicationDbContext _context;

        public DeleteStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Do(int id)
        {
            var stock = _context.Stock.FirstOrDefault(x => x.Id == id);
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return true;
        } 
    }
}