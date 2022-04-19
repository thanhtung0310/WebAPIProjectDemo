using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIProjectDemo.Models;

namespace WebAPIProjectDemo
{
    public class EmployeeSecurity
    {
        public static EmployeeDBContext entities = new EmployeeDBContext();
        public static bool Login(string username, string pwd)
        {
            return entities.accounts.Any(u => u.acc_username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.acc_password == pwd);
        }
    }
}