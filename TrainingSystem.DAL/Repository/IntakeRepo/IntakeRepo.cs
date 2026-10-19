using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingSystem;

public class IntakeRepo : GenericRepo<Intake>, IIntakeRepo
{
    private readonly AppDbContext context;
    public IntakeRepo(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
