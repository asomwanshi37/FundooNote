using FundooModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Context
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<RegisterModel> User { get; set; }
        public DbSet<NotesModel> Notes { get; set; }
    }
}
