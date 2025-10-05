using Route02.DAL.Models.Shared;

namespace Route02.DAL.Repositories.Interfaces;

public interface IGenericRepository<T> where T: BaseEntity
{
    // Get all employee 
    IEnumerable<T> GetAll (bool withTracking = false);
    
    //  Get By id
    T? GetById (int? id);
    
    // Add
    void Add (T entity);
    
    // Update
    void Update (T entity);
    
    // Delete
    void Delete (T entity);
}