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

        public Task<List<Component>> GetAllAsync() => _componentRepository.GetAllAsync();
        public Task<Component?> GetByIdAsync(Guid id) => _componentRepository.GetByIdAsync(id);
        public Task<Component> AddAsync(Component component) => _componentRepository.AddAsync(component);
        public Task<bool> UpdateAsync(Component component) => _componentRepository.UpdateAsync(component);
        public Task<bool> DeleteAsync(Guid id) => _componentRepository.DeleteAsync(id);
    }
}
