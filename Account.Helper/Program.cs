// See https://aka.ms/new-console-template for more information
using CardStorageService;


var res = PasswordUtils.CreatePasswordHash("12345");
Console.WriteLine(res);
Console.ReadKey(true);