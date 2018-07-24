using Philanski.Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Philanski.Backend.Library.Repositories
{
    public class Repository
    {

        private readonly PhilanskiManagementSolutionsContext _db;

        /// <summary>
        /// Initializes a new philanski management solutions repo given a suitable Entity Framework DbContext.
        /// </summary>
        /// <param name="db">The DbContext</param>
        public Repository(PhilanskiManagementSolutionsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        //public int testGetEmployee()
        //{
        //    int herndoninvfromdb = (from employees in _db.Employees
        //                            select herndoninv.StoredPizza).SingleOrDefault();

        //    return herndoninvfromdb;

        //}


    }
}
