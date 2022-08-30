// See https://aka.ms/new-console-template for more information
using JwtSample;

Console.WriteLine("Enter name");
string user = Console.ReadLine();
Console.WriteLine("Enter password");
string password = Console.ReadLine();

UserService userService = new UserService();
string token = userService.Authenticate(user, password);
Console.WriteLine(token);
Console.ReadKey(true);

