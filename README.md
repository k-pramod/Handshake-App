# Handshake Technologies
## Presents Handshake, a networking and recruiting tool

Thank you for visiting our GitHub repository. Here you will find our Web API and Android App as well as a prototype of our flagship technology on a Raspberry Pi: handshaking to form a connection with another individual.

Our Web API is strongly integrated with Microsoft Technologies: ASP.NET MVC + Web APIs, Microsoft SQL. We have also exposed an API for others to interact with our service.

We also have developed an Android App and prototype handshaking script with Raspberry Pi available in this repository.

Our app is live at [Handshake.AzureWebsites.net]('Handshake.AzureWebsites.net').

## Web API

You can POST and GET data from our service with these methods:
* POST: `/Users/Login` with `String email, String` returns `password int userId`
* POST: `/Users/GetConnectionsAsString`	with `long userId` returns `String cards`
* POST: `/Handshakes/Log` with `long userId` `bool`
	RETURNS:
		TRUE if handshake made
		FALSE if handshake not made

Example:
POST: `http://handshake.azurewebsites.com/Users/Login?email=tom&password=tom`

## Contact

Please contact us at [david.alanis@gatech.edu]('mailto:david.alanis@gatech.edu') with any questions or inquires.
