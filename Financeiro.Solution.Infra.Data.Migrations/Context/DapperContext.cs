
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;

            
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("SqlConnection"));

        public IDbConnection CreateMasterConnection()
            => new SqlConnection(_configuration.GetConnectionString("MasterConnection"));

    }
}


