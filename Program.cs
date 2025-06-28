using Public_Api_Demo.Interfaces;
using Public_Api_Demo.Models;
using Public_Api_Demo.Services;

namespace Public_Api_Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("------- Executing of the public Api have been started--------");

            IUserService userService = new UserService();
            Console.Write("Enter user ID to fetch: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            var user = await userService.GetUserById(userId);

            if (user != null)
            {
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Avatar: {user.Avatar}");
            }
            else
            {
                Console.WriteLine("User not found or error occurred.");
            }
            var Users = await userService.GetAllUsers(1);
           
                foreach (var item in Users)

                {
                    Console.WriteLine($"ID: {item.Id} | Name: {item.FirstName} {item.LastName} | Email: {item.Email}");

                }
            

            


        }
    }
}
