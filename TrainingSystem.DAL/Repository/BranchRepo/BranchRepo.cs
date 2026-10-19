using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class BranchRepo : GenericRepo<Branch>, IBranchRepo
{
    private readonly AppDbContext context;
    public BranchRepo(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
