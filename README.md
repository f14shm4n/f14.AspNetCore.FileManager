# NOT ACTUAL, NEW DESCRIPTION IN PROGRESS

# Intro

Initially, I created this project as the back-end (ASP.NET Core) part of the my own web file manager.

But you can use it as a backend for any other application with similar functionality.

### What is it?

This lib provides several base handlers for the file manager requests.

The each handler designed as service for the ServiceCollection to use as Dependency Injection.

# Install

[Nuget](https://www.nuget.org/packages/f14.AspNetCore.FileManager/): `PM> Install-Package f14.AspNetCore.FileManager -Version 1.1.1`

# How To Use

1) First of all you need add a services:

```
    using f14.AspNetCore.FileManager; // Add namespace

    public void ConfigureServices(IServiceCollection services)
    {
        // Adds all default handlers as services.
        // 1) Use this if you want to parse http request data by yourself.
        // services.AddFileManagerHandlers();
        // 2) Use this if you want to the service parse the http request data.
        // services.AddFileManagerJsonFileManager();
    }
```

2) Then you need use it in your controller which used for handling request from a file manager:

```
    // Your controller for file manager
    public class FileManagerController : Controller
    {
        // Method to handler request from fronend
        // Use IOperationHandler if you used first method of service adding in the 1 step.
        [HttpPost]
        public JsonResult CreateFolder([FromServices]IOperationHandler<CreateFolderParam> handler)
        {
            // Get json request data
            string requestBody = Request.ReadBody();
            BaseResult result = null;
            try
            {                
                var param = JsonConvert.DeserializeObject<CreateFolderParam>(requestBody);
                result = handler.Run(param);
            }
            catch (Exception ex)
            {
                // log errors
                return BadRequest(); // Or what you need
            }
            return Json(result); // Write result.
        }
        
        === OR ===
        
        // Method to handler request from fronend
        // Use IJOperationHandler if you used second method of service adding in the 1 step.
        [HttpPost]
        public JsonResult CreateFolder([FromServices]IJOperationHandler<CreateFolderParam> handler)
        {
            // Get json request data
            string requestBody = Request.ReadBody();
            BaseResult result = null;
            try
            {                
                result = handler.Run(requestBody);
            }
            catch (Exception ex)
            {
                // log errors
                return BadRequest(); // Or what you need
            }
            return Json(result); // Write result.
        }
    }
```

Do the same thing for the other operation handlers.

# List of build-in operation handlers

`IOperationHandler<CopyParam>` - for copy files and folders.
    
`IOperationHandler<MoveParam>` - for move files and folders.

`IOperationHandler<CreateFolderParam>` - for create folders.

`IOperationHandler<DeleteParam>` - for delete files and folders.

`IOperationHandler<FolderStructParam>` - for receiving file system map.

`IOperationHandler<RenameParam>` - for rename files and folders.

Or the same but with `IJOperationHandler<T>` for usin build-in json parsing method.

# Customizing

You can of course create additional handlers or overwrite the default handlers.

### Custom handler

To create new handlers you can use following classes and interfaces.

`IOperationHandler` - the base interface for the handler.

`IJOperationHandler` - the next level of the interface for the handler provides json parsing functions.

`BaseOperationHandler` - the abstract class which include base implementation of interfaces above. Use this class as a foundation.

Sample:

```
    // Create you handler class
    public class SampleIOHandler : BaseMoveHandler<SampleIOParam>
    {
        public SampleIOHandler(IHostingEnvironment env) : base(new SampleIOResult(), env)
        {
        }

        public override BaseResult Run(SampleIOParam param)
        {
            // Do any useful action here
            // Set some useful to the result
            Result.Var1 = Useful result;
            Result.Var2 = Useful result;
            <...>
            return Result; // Return result.
        }
    }
    
    
    <...........................................>
    
    // Add your handler into the service collection.    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IJOperationHandler<SampleIOParam>, SampleIOHandler>();
    }
    
    <...........................................>
    
    // Use you handler what ever you need it. See #How To Use section.
```

###

Also you can replace the default handler by the your handler.

```
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddFileManagerJsonFileManager(); // Use default handlers
        services.AddTransient<IJOperationHandler<CreateFolderParam>, SampleIOHandler>(); // Overwrite default handler
    }
```

## Authors

* [f14shm4n](https://github.com/f14shm4n)

## License

[MIT](https://opensource.org/licenses/MIT)
