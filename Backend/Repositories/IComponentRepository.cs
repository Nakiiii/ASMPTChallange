using Backend.Models;

namespace Backend.Repositories
{
    public interface IComponentRepository
    {
        Task<List<Component>> GetAllComponentsAsync();
        Task<Component?> GetComponentByIdAsync(Guid id);
        Task<Component> AddComponentAsync(Component component);
        Task<bool> UpdateComponentAsync(Guid id, Component component);
        Task<bool> DeleteComponentAsync(Guid id);
    }
}
