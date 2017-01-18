//using humanresource.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //AttUser user = new AttUser();
            //user.EmployeeName = "Sintak";
            //Console.WriteLine(user.EmployeeName);

            Test();

            Console.ReadKey();

        }


        static async void Test()
        {
            var a = await TestAsync();
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }

        }


        static Task<IEnumerable<int>> TestAsync()
        {
            Task<IEnumerable<int>> task = new Task<IEnumerable<int>>(Get);

            task.Start();

            return task;
        }

        static IEnumerable<int> Get()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i);
                if (i> 2)
                    yield return i;

                if (i > 10)
                    yield break;
            }
        }
    }
}
