using System;
using System.Collections.ObjectModel;

namespace LiveShapingDemo
{
    public class CustomerCollection : ObservableCollection<Customer>
    {
        private int id;
        private Random rand;

        private CustomerCollection()
        {
            id = 1;
            rand = new Random();
            PopulateDefaultCustomers();
        }

        public static CustomerCollection Create()
        {
            return new CustomerCollection();
        }

        private void PopulateDefaultCustomers()
        {
            Add(CreateCustomer("Pete Brown"));
            Add(CreateCustomer("Mighty Ralph"));
            Add(CreateCustomer("Diana Prince"));
            Add(CreateCustomer("David Rachel"));
            Add(CreateCustomer("John Burger"));
            Add(CreateCustomer("Janis Hope"));
            Add(CreateCustomer("Manchester Williams"));
            Add(CreateCustomer("Hope Springs"));
            Add(CreateCustomer("Linda Worthmire"));
            Add(CreateCustomer("Eleanor Ranch"));
            Add(CreateCustomer("Sean Lip"));
            Add(CreateCustomer("Peace Ball"));
            Add(CreateCustomer("Moon Girl"));
            Add(CreateCustomer("Hellen Barbary"));
            Add(CreateCustomer("Jimmy of Calcutta"));
            Add(CreateCustomer("Nancy Peterson"));
            Add(CreateCustomer("Rafael Mendosa"));
            Add(CreateCustomer("Lupita Mendosa"));
            Add(CreateCustomer("Marty Holenbrock"));
        }

        private Customer CreateCustomer(string name)
        {
            return new Customer(id++, name)
            {
                Balance = rand.Next(Customer.MinBalance, Customer.MaxBalance + 1)
            };
        }
    }
}