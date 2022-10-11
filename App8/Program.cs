using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App8
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            Groupring2();
            Groupring2L();
            Console.Read();
        }
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        static void IntroToLINQL()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = numbers.Where(x => (x % 2) == 0);

            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        static void DataSource()
        {
            var queryAllCustomers = 
                from cust in context.clientes
                select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void DataSourceL()
        {
            var queryAllCustomers = context.clientes.ToList();

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Filtering()
        {

            var queryLondonCustomers =
                from cust in context.clientes
                where cust.Ciudad == "Londres"
                select cust;

            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void FilteringL()
        {

            var queryLondonCustomers = context.clientes.Where(x => x.Ciudad == "Londres");

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Oldering()
        {
            var queryLondonCustomers3 =
                from cust in context.clientes
                where cust.Ciudad == "Londres"
                orderby cust.NombreCompañia ascending
                select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void OlderingL()
        {

            var queryLondonCustomers3 = context.clientes.Where(x => x.Ciudad == "Londres")
                .OrderBy(x=>x.NombreCompañia);

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Groupring()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("    {0}", customer.NombreCompañia);
                }
            }
        }
        static void GroupringL()
        {

            var queryCustomersByCity = context.clientes.GroupBy(x => x.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("    {0}", customer.NombreCompañia);
                }
            }
        }
        static void Groupring2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Groupring2L()
        {

            var custGroup = context.clientes.GroupBy(x => x.Ciudad);
            var custQuery = custGroup.Where(x => x.Count() > 2)
                .OrderBy(x => x.Key);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        static void JoiningL()
        {

            var innerJoinQuery = context.clientes
                .Join(context.Pedidos, 
                x => x.idCliente, y => y.IdCliente, 
                (x, y) => new { CustomerName = x.NombreCompañia, DistributorName = y.PaisDestinatario });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
