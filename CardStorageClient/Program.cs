
using Grpc.Net.Client;
using static ClientServiceProtos.ClientService;

Console.WriteLine("Ввод клиента ...");
Console.ReadKey();

using (GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:80"))
{
    ClientServiceClient client = new ClientServiceClient(channel);

    var response = client.Create(new ClientServiceProtos.CreateClientRequest {
        FirstName = "Иван",
        Patronymic = "Иванович",
        Surname = " Иванов"
    });

    Console.WriteLine($"{response.ClientId},   {response.ErrorCode},  {response.ErrorMessage}");
}
    


