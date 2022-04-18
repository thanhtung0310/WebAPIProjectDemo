using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIProjectDemo.Models;

namespace WebAPIProjectDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        private EmployeeDBContext entities = new EmployeeDBContext();

        /// <summary>
        /// Get list of all Employees
        /// </summary>
        /// <remarks>
        /// Get JSON list of all Employees
        /// </remarks>
        /// <returns></returns>
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<employee> GetEmployeeList()
        {
            return entities.employees.ToList();
        }

        /// <summary>
        /// Get list of all Employees with parameter {floor}
        /// </summary>
        /// <remarks>
        /// Get JSON list of all Employees with parameter {floor}
        /// </remarks>
        /// <returns></returns>
        // GET api/<controller>/{floor}
        //[HttpGet]
        //public HttpResponseMessage GetEmployeeListByPara(string position = "All")
        //{
        //    switch (position.ToLower())
        //    {
        //        case "all":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.employees.ToList());
        //        case "ground floor":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.employees.Where(e => e.emp_position == "ground floor").ToList());
        //        case "second floor":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.employees.Where(e => e.emp_position == "second floor").ToList());
        //        case "third floor":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.employees.Where(e => e.emp_position == "third floor").ToList());
        //        case "fourth floor":
        //            return Request.CreateResponse(HttpStatusCode.OK, entities.employees.Where(e => e.emp_position == "fourth floor").ToList());
        //        default:
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, "Value for position must be ALL, Ground/Second/Third/Fourth Floor." + position + " is Invalid!!");
        //    }
        //}

        /// <summary>
        /// Get info of one Employee
        /// </summary>
        /// <remarks>
        /// Get JSON list of one Employee
        /// </remarks>
        /// <returns></returns>
        // GET api/<controller>/id
        [HttpGet]
        public HttpResponseMessage GetEmployeeByID(int id)
        {
            var entity = entities.employees.FirstOrDefault(e => e.emp_id == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found!!");
            }
        }

        /// <summary>
        /// Create new info for one Employee
        /// </summary>
        /// <remarks>
        /// Create new info for one Employee
        /// </remarks>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage InsertNewEmployee([FromBody] employee e)
        {
            try
            {
                entities.employees.Add(e);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, e);
                message.Headers.Location = new Uri(Request.RequestUri + e.emp_id.ToString());
                return message;
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Update info of one Employee with {id}
        /// </summary>
        /// <remarks>
        /// Update info of one Employee with {id}
        /// </remarks>
        /// <returns></returns>
        // PUT api/<controller>/{id}
        [HttpPut]
        public HttpResponseMessage UpdateEmployeeByID(int id, [FromBody] employee employee)
        {
            try
            {
                var entity = entities.employees.FirstOrDefault(e => e.emp_id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found!!");

                }
                else
                {
                    entity.emp_name = employee.emp_name;
                    entity.emp_position = employee.emp_position;
                    entity.emp_dob = employee.emp_dob;
                    entity.emp_contact_number = employee.emp_contact_number;
                                        
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Delete one Employee with {id}
        /// </summary>
        /// <remarks>
        /// Delete one Employee with {id}
        /// </remarks>
        /// <returns></returns>
        // DELETE api/<controller>/{id}
        [HttpDelete]
        public HttpResponseMessage DeleteEmployeeByID(int id)
        {
            try
            {
                var entity = entities.employees.FirstOrDefault(e => e.emp_id == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found!!");

                }
                else
                {
                    entities.employees.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Employee with Id = " + id.ToString() + " has been deleted!!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }            
        }
    }
}