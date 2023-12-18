namespace Model.Repositories;

public interface IRepository<T> where T:class
{
    List<T> GetList();
    
}