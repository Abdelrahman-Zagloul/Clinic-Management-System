namespace Clinic_Management_system.Services.Interfaces
{
    public interface ICRUDService<T>
    {
        (bool isSuccess, string message) Add(T type);

        (bool isSuccess, string message) Update(int id, T newType);

        (bool isSuccess, string message) Delete(int id);

        (bool isSuccess, string message) GetById(int id);

        (bool isSuccess, string message) GetAll();

        (bool isSuccess, string message) Clear();
    }
}