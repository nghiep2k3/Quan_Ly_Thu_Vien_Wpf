using Book_Management;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Controls.Primitives;

namespace Book_Management
{
     public class Account
    {
        private static Account instance;
        public static Account Instance
        {
            get { if (instance == null) instance = new Account(); return instance; }
            private set { instance = value; }
        }

        private Account()
        { }

        public bool Login(string username, string password)
        {
                string query = "SELECT * FROM TAIKHOAN WHERE USERNAME = @userName AND PASS_WORD = @passWord";
                DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
                return result.Rows.Count > 0;

        }
    }
}
