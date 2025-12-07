using Backend.Models;
using Backend.Repositories;

namespace Backend.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _componentRepository;

        public ComponentService(IComponentRepository componentRepository)
        {
            _componentRepository = componentRepository;
        }

        public Task<List<Component>> GetAllComponentsAsync() => _componentRepository.GetAllComponentsAsync();
        public Task<Component?> GetComponentByIdAsync(Guid id) => _componentRepository.GetComponentByIdAsync(id);
        public Task<Component> AddComponentAsync(Component component) => _componentRepository.AddComponentAsync(component);
        public Task<bool> UpdateComponentAsync(Guid id, Component component) => _componentRepository.UpdateComponentAsync(id, component);
        public Task<bool> DeleteComponentAsync(Guid id) => _componentRepository.DeleteComponentAsync(id);
    }
}
