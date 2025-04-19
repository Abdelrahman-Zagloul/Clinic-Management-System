namespace Clinic_Management_system.Repository.Interfaces
{
    public interface ICRUDRepository<T>
    {
        int Add(T type);

        bool Update(int id, T type);

        bool Delete(int id);

        T? GetById(int id);

        List<T> GetAll();

        bool Clear();
    }
}