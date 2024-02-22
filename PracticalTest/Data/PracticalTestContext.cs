using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Models;

    public class PracticalTestContext : DbContext
    {
        public PracticalTestContext (DbContextOptions<PracticalTestContext> options)
            : base(options)
        {
        }

        public DbSet<PracticalTest.Models.Product> Product { get; set; } = default!;
    }
