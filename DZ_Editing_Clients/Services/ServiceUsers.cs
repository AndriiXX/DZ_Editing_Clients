using Microsoft.EntityFrameworkCore;
using DZ_Editing_Clients.Models;

namespace DZ_Editing_Clients.Services
{
    public interface IServiceUsers
    {
        public Task<User?> CreateAsync(User? user);
        public Task<IEnumerable<User>> ReadAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task<User?> UpdateAsync(int id, User? user);
        public Task<bool> DeleteAsync(int id);
    }
    public class ServiceUsers : IServiceUsers
    {
        private readonly UserssContext _userContext;
        private readonly ILogger<ServiceUsers> _logger;
        public ServiceUsers(
            UserssContext userContext,
            ILogger<ServiceUsers> logger
            )
        {
            _userContext = userContext;
            _logger = logger;
        }
        public async Task<User?> CreateAsync(User? user)
        {
            if (user == null)
            {
                _logger.LogWarning("Attempt is created product with null");
                return null;
            }
            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _userContext.Users.FindAsync(id);
            if (product == null)
            {
                _logger.LogInformation("Not found product");
                return false;
            }
            _userContext.Users.Remove(product);
            await _userContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> ReadAsync()
        {
            return await _userContext.Users.ToListAsync();
        }

        public async Task<User?> UpdateAsync(int id, User? user)
        {
            if (user == null || id != user.Id)
            {
                _logger.LogWarning("if (product == null || id != product.Id)");
                return null;
            }
            try
            {
                _userContext.Users.Update(user);
                await _userContext.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
