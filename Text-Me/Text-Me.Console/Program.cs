// See https://aka.ms/new-console-template for more information

using System.Net;
using Text_Me.Service;
using static Text_Me.Service.Client;

Client Client = new Client();

Client.Connect(IPAddress.Loopback.ToString(), 63909);

Console.ReadLine();


void ConnectionResultCallback(ConnectionResult result)
{
    Console.WriteLine(result);
}