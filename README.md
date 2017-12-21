# Intro

Initially, I created this project as the back-end (ASP.NET Core) part of the my own web file manager.

But you can use it as a backend for any other application with similar functionality.

### What is it?

This lib provides several base handlers for the file manager requests.

The each handler designed as service for the ServiceCollection to use as Dependency Injection.

# Install

[Nuget](https://www.nuget.org/packages/f14.AspNetCore.FileManager/): `PM> Install-Package f14.AspNetCore.FileManager`

# How To Use

First off all learn the [format](https://github.com/f14shm4n/f14.AspNetCore.FileManager/blob/master/JsonFormat.md).

1) Add a services:

```
    using f14.AspNetCore.FileManager; // Add namespace

    public void ConfigureServices(IServiceCollection services)
    {
        // Adds all default handlers as services.        
        services.AddFileManagerHandlers();
    }
```

2) Then you need use it in your controller which used for handling request from a file manager:

```
    // Your controller for file manager
    public class FileManagerController : Controller
    {       
        // Method to handler request from fronend
        [HttpPost]
        public IActionResult CreateFolder([FromServices]ICreateFolderHandler handler)
        {
            // Get json request data
            string requestBody = Request.ReadBody();
            try
            {
                BaseResult result = handler.Run(requestBody);
                return Json(result); 
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

# List of build-in operation handlers

`ICopyHandler` - for copy files and folders.
    
`IMoveHandler` - for move files and folders.

`ICreateFolderHandler` - for create folders.

`IDeleteHandler` - for delete files and folders.

`IFolderStructHandler` - for receiving file system map.

`IRenameHandler` - for rename files and folders.

# Customizing

You can create additional handlers or overwrite the default handlers.

### Custom handler

To create new handlers you can use following classes and interfaces.

`IOperationHandler` - the base interface for the handler.

`IJOperationHandler` - the next level of the interface for the handler provides json parsing functions.

`BaseOperationHandler` - the abstract class which include base implementation of interfaces above. **Use this class as a base to start.

Sample:

```
    // Create you handler class
    public class SampleHandler : BaseOperationHandler<SampleParam, SampleResult>
    {
        public SampleHandler(IHostingEnvironment env, < or any other DI >) : base(new SampleResult(), env.WebRootPath)
        {
        }

        public override SampleResult Run(SampleParam param)
        {
            // Do any useful action here
            // Set some useful data into the result
            Result.Var1 = Useful result;
            Result.Var2 = Useful result;
            <...>
            // Return result.
            return Result; 
        }
    }
    
    
    <...........................................>
    
    // Add your handler into the service collection.    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IJOperationHandler<SampleParam, SampleResult>, SampleHandler>();
    }
    
    <...........................................>
    
    // Use your handler in the controller. See #How To Use section.
    [HttpPost]
    public JsonResult SampleAction([FromServices]IJOperationHandler<SampleParam, SampleResult> handler)
    {
        ...
    }
    
```

### Overwrite default handler

Also you can replace the default handler by the your handler.

```
    public void ConfigureServices(IServiceCollection services)
    {
        // Use default handlers
        services.AddFileManagerJsonHandlers();
        // Overwrite default handler
        services.Replace(new ServiceDescriptor(typeof(ICopyHandler), typeof(CustomCopyHandler), ServiceLifetime.Transient));
    }
```

## Authors

* [f14shm4n](https://github.com/f14shm4n)

## License

[MIT](https://opensource.org/licenses/MIT)
