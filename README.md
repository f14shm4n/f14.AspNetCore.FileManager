# Intro

This project created as a backend part for the file manager app.

[Here](https://github.com/f14shm4n/FManager) is the fronend part. *(The frontend application does not support backend with version **>=2.0.0**)*

The application is built using the CQRS pattern and [MediatR](https://github.com/jbogard/MediatR) library to manage command and request handlers.

## Install

[Nuget](https://www.nuget.org/packages/f14.AspNetCore.FileManager/): `PM> Install-Package f14.AspNetCore.FileManager`

## How To Use

First off all learn the [format](https://github.com/f14shm4n/f14.AspNetCore.FileManager/blob/master/JsonFormat.md).

1) Add a services:

```csharp
    using f14.AspNetCore.FileManager; // Add namespace

    public void ConfigureServices(IServiceCollection services)
    {
        // Adds all default handlers as services.        
        services.AddMediatR(typeof(FileManagerAssemblyBeacon).Assembly);
    }
```

2) Then you need use it in your controller which used for handling request from a file manager:

```csharp
// Your controller for file manager
public class FileManagerController : Controller
{   
    private IMediator _mediator;

_   public FileManagerController(IMediator mediator)
    {
        _mediator = mediator;    
    } 

    // Method to handler request from fronend
    [HttpPost]
    public async Task<IActionResult> CreateFolder(CreateCommand command)
    {
        try
        {
            var r = await _mediator.Send(command);            
            return Ok(); // Or what you need
        }
        catch (Exception ex)
        {
            // log errors
            return BadRequest(); // Or what you need
        }            
    }
}
```

Do the same thing for the other operation handlers.

## Commands and Queries

`CopyCommand` - for copy files and folders.
    
`MoveCommand` - for move files and folders.

`CreateCommand` - for create folders.

`DeleteCommand` - for delete files and folders.

`RenameCommand` - for rename files and folders.

`GetFolderStructureQuery` - for receiving file system map.


## Overwrite handlers

To overwrite the default handlers, you need to use the default method provided by the Asp.Net Core DI container.

```csharp
public class Startup
{
    [Fact]
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(typeof(FileManagerAssemblyBeacon).Assembly);
        // Call your overwrite code after calling the AddMediatR().
        services.Replace(new ServiceDescriptor(typeof(IRequestHandler<CreateCommand, CreateCommandResult>), typeof(CustomCreateHandler), ServiceLifetime.Transient));
    }

    ... Caustom handler

    public class CustomCreateHandler : IRequestHandler<CreateCommand, CreateCommandResult>
    {
        public Task<CreateCommandResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
```

## Authors

* [f14shm4n](https://github.com/f14shm4n)

## License

[MIT](https://opensource.org/licenses/MIT)
