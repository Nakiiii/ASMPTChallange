using Backend.Models;

namespace Backend.Services
{
    public interface IComponentService
    {
        Task<List<Component>> GetAllComponentsAsync();
        Task<Component?> GetComponentByIdAsync(Guid id);
        Task<Component> AddComponentAsync(Component component);
        Task<bool> UpdateComponentAsync(Guid id, Component component);
        Task<bool> DeleteComponentAsync(Guid id);
    }
}
