using System.Net;
using Text_Me.Service;



int serverPortNum = 49303;


Client client = new Client();
client.OnConnection += ClientConnection;
client.OnMessageReceived += ClientMessageReceived;

client.Connect(IPAddress.Loopback.ToString(), serverPortNum);


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

    client.SendMessage(input);
}


void ClientConnection(ConnectionResult result)
{
    Console.WriteLine($"Connection resulted in {result}");
}
void ClientMessageReceived(string receivedMessage)
{
    Console.WriteLine($"Received: {receivedMessage}");
}