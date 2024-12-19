using RandomAPI.Model;

namespace RandomAPI.Service
{
    public class FruitsService
    {
        private readonly RandomDbContext _dbContext;

        public FruitsService(RandomDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Fruit> GetAllFruit()
        {
            return _dbContext.Fruits.ToList();
        }
    }
}
