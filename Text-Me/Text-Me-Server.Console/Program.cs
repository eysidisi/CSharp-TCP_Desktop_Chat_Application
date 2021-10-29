using System.Net;
using Text_Me.Service;

int serverPortNum = 49303;

Server server = new Server(IPAddress.Loopback.ToString(), serverPortNum);
server.OnConnection += ClientConnection;
server.OnMessageReceived += ClientMessageReceived;

server.StartAcceptingConnection();

while (true)
{
    string? input = Console.ReadLine();

    if (input == null)
    {
        continue;
    }
    if (input == "qq")
    {
        break;
    }

    server.SendMessage(input);
}


void ClientConnection(ConnectionResult result)
{
    Console.WriteLine($"Connection resulted in {result}");
}
void ClientMessageReceived(string receivedMessage)
{
    Console.WriteLine($"Received: {receivedMessage}");
}