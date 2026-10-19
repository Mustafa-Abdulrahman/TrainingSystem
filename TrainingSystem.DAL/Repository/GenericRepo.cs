using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    private readonly AppDbContext context;
    public GenericRepo(AppDbContext _context)
    {
        context = _context;
    }

    public void Add(T item)
    {
        context.Add(item);
    }

    public void Delete(T item)
    {
        context.Remove(item);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public void Update(T item)
    {
        context.Update(item);
    }
}
