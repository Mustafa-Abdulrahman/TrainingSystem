using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public interface IGenericRepo<T> where T: class
{
    public Task<List<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public void Add(T item);
    public void Update(T item);
    public void Delete(T item);
}
