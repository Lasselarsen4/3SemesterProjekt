﻿using System.Collections.Generic;
using ModelAPI;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface ICustomerLogic
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}