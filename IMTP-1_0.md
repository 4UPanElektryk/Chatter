# Internet Message Transfer Protocol Documentation

## Overview
The protocol described here is used for communication with a server using the IMTP (Internet Message Transfer Protocol). All communication with the server must adhere to the specified version and follow a specific path. The data exchanged between the client and server is formatted as JSON (JavaScript Object Notation).

## Protocol Version
The current version of the protocol is `1.0`.

## Server Path
The server path defines the endpoint to which the client must send requests. The server path is as follows:
```
/path
```

## Request Format
All requests sent to the server must adhere to the following format:

```
IMTP/Version Path
{
  "key1": "value1",
  "key2": "value2",
  ...
}
```

The request starts with the `IMTP/Version Path` line, where `Version` is the desired protocol version and `Path` is the server path mentioned above. The request body contains the data in JSON format, where each key-value pair represents a specific parameter or information to be sent to the server.

## Response Format
The server responds to requests with a JSON-formatted response. The response format is as follows:

```
Status Code/Message
{
	"key1": "value1",
	"key2": "value2",
	...
}
```

The response includes a `Status Code/Message` line indicating the outcome of the request. The `Status Code` represents the result of the request, while the `Message` provides a brief description of the response. The `data` field contains the server's response data, if applicable.

## Status Codes
The following status codes are used in the protocol:

- 0: OK
- 1: Not Found
- 2: Not Supported
- 3: Incorrect Data
- 4: Authentication Needed
- 5: Authentication Error
- 6: Forbidden
- 2x range: Internal Server Errors

## Examples

### Example Request
```
IMTP/1.0 /login
{
  "username": "john_doe",
  "password": "secretpassword"
}
```

### Example Response
```
0/OK
{
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
}
```

### Example Error Response (Status Code 1 - Not Found)
```
1/Not Found
{
	"Path": "/your_path"
}
```

In this example, the server responds with a status code of 1 and the message "Not Found" to indicate that the requested resource was not found. The `data` field is empty in this case.

## Conclusion
This documentation provides an overview of the protocol used to communicate with the server using the Internet Message Transfer Protocol. Please ensure that all requests follow the specified format and include the necessary information for successful communication. The server will respond with the appropriate status code and message to indicate the outcome of the request.