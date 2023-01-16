using AnimalsController.Models;

namespace AnimalsController.Services
{
    public interface IDatabaseService
    {
        IEnumerable<Animal> GetAnimals(string orderBy);
        void AddAnimal(Animal newAnimal);
        bool UpdateAnimal(int idAnimal, Animal updatedAnimal);
        bool DeleteAnimal(int idAnimal);
    }
}
