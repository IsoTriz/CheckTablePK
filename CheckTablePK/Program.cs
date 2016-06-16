using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace CheckTablePK
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "Data Source=(local); Initial Catalog=MedpracEmpty; User ID = sa; Password = poi22; Integrated Security=True";
            SqlConnection serverConn = new SqlConnection(a);
            serverConn.Open();

            DataTable dt = serverConn.GetSchema("Tables");
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("888_Scope");

            foreach (DataRow row in dt.Rows)
            {
                string tableName = (string)row[2];
                string tableType = (string)row[3];

                if (tableType == "VIEW") continue;

                DbSyncTableDescription tblPatientDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(tableName, serverConn);

                // add the table description to the sync scope definition
                scopeDesc.Tables.Add(tblPatientDesc);

                if (scopeDesc.Tables[tableName].PkColumns.Count() == 0)
                {
                    Console.WriteLine(tableName);
                }
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
