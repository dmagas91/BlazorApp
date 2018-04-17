using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.DAL.EF
{
    public interface IDbService
    {
        DbContext GetDbContext();
    }
}
