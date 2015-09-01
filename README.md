# FireLite
### A Lightweight TCP Socket Server/Client Library
FireLite is an extremely lightweight TCP Socket Server/Client Library that enables you to quickly and easily setup client/server communication in your .NET applications.

FireLite was built primarily to facilitate the creation of a lightweight online multiplayer game and so it's key functionality is to provide a simple and elegant solution to transferring raw bytes over a TCP connection.

## Usage
### Creating a Basic Server
To create a basic server, you simply inherit from `FireLite.Core.Network.Server`.

```
public class BasicServer : FireLite.Core.Network.Server
{
  public BasicServer(int port) : base(port) { }
}
```

You can then start your server in a Console Application as follows:

```
static void Main(string[] args)
{
  var server = new BasicServer(1337);
  server.Start();

  Console.WriteLine("Press any key to stop the server.")
  Console.ReadKey();
  server.Stop();
}
```

#### The Server Class
The `Server` class is an abstract base implementation that must be inherited from.

##### Public/Protected Properties

* `public int Port`
* `protected IList<ClientConnection> ConnectedClients`

##### Public/Protected Methods

* `public void Start()`
* `public void Stop()`
* `protected virtual void OnStarted()`
* `protected virtual void OnStopped()`
* `protected virtual void OnClientConnected(ClientConnection)`
* `protected virtual void OnClientDisconnected(ClientConnection)`
* `protected virtual void OnClientPacketReceived(ClientConnection, byte[])`

#### The ClientConnection Class
The `ClientConnection` class represents a single connected client.

##### Public/Protected Properties
* `public Guid Id`

##### Public/Protected Methods

* `public void SendPacket(byte[])`
* `public void Disconnect()`

#### Overriding the Server's Functionality
There are several protected methods available for you to override which allow you to customize how your server interacts with clients. These are:

* `protected virtual void OnStarted()`
* `protected virtual void OnStopped()`
* `protected virtual void OnClientConnected(ClientConnection)`
* `protected virtual void OnClientDisconnected(ClientConnection)`
* `protected virtual void OnClientPacketReceived(ClientConnection, byte[])`

An example use case of overriding one of these methods would be to send a welcome message to a client when they connect to the server.

```
protected override void OnClientConnected(ClientConnection clientConnection)
{
  var message = "Hello, client. Welcome to the server".GetBytes();
  clientConnection.SendPacket(message);
}
```

## Creating a Basic Client
To create a basic client, you simply inherit from `FireLite.Core.Network.Client`.

```
public class BasicClient : FireLite.Core.Network.Client
{
  public BasicClient(string host, int port) : base(host, port) { }
}
```

You can then connect your client to a server in a Console Application as follows:

```
static void Main(string[] args)
{
    var client = new BasicClient("127.0.0.1", 1337);
    client.Connect();

    Console.WriteLine("Press any key to disconnect");
    Console.ReadKey();
    client.Disconnect();
}
```

#### The Client Class
The `Client` class is an abstract base implementation that must be inherited from.

##### Public/Protected Properties
* `public string Host`
* `public int Port`

##### Public/Protected Methods
* `public bool Connect()`
* `public void Disconnect()`
* `public void SendPacket(byte[])`
* `protected virtual void OnConnected()`
* `protected virtual void OnConnectionFailed()`
* `protected virtual void OnDisconnected()`
* `protected virtual void OnPacketReceived(byte[])`

#### Overriding the Client's Functionality
There are several protected methods available for you to override which allow you to customize how your client interacts with the server. These are:

* `protected virtual void OnConnected()`
* `protected virtual void OnConnectionFailed()`
* `protected virtual void OnDisconnected()`
* `protected virtual void OnPacketReceived(byte[])`

An example use case of overriding one of these methods would be to send a response back to the server when you receive a specific message.

```
protected override void OnPacketReceived(byte[] packetBytes)
{
  var message = packetBytes.GetString();
  if (message == "Hello, world!")
  {
    var response = "Oh hi, server!".GetBytes();
    SendPacket(response);
  }
}
```

## Utility Extension Methods
FireLite comes with a couple extension methods that facilitate sending UTF8 encoded strings over the TCP connection. These include:

* string GetString(this byte[])
* byte[] GetString(this string)

It also comes with a few `NetworkStream` extension methods that facilitate sending and receiving TCP packets. These are really only meant to be used internally by `FireLite.Core` classes, but they have been made public should you wish to use them for any reason. These include:

* `byte[] ReadNBytes(this NetworkStream)`
* `byte[] ReadPacket(this NetworkStream)`
* `void SendPacket(this NetworkStream, byte[])`
