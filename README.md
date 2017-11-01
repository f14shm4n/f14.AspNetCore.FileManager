# Intro

Initially, I created this project as the back-end (ASP.NET Core) part of the my own web file manager.

But you can use it as a backend for any other application with similar functionality.

### How it can be?

Simply, this project does not contains any controllers and routes and etc, but contains only services that can be used as DI in ASP.NET Core. 

This means that everything related to controllers and paths is your task.

This allows me to focus on the target functionality of this project, and allows you not to search, guess which controllers I shoved in the library and what routes I set up.

# Install

Nuget: `nuget`

# How To Use

[(Guide) How to use with FManager](https://github.com/f14shm4n/FManager)

The following is a general overview of the usage.

1) First of all you need add a services:

```
    using f14.AspNetCore.FileManager; // Add namespace

    public void ConfigureServices(IServiceCollection services)
    {
        // Add for default
        services.AddF14FileManager();  
    }
```

2) Then you need use it in your controller which used for handling request from a file manager:

```
    // Your controller for file manager
    public class FileManagerController : Controller
    {
        // Method to handler request from fronend
        [HttpPost]
        public IActionResult AjaxAction([FromServices]RequestHandlerManager rhManager, [FromServices] IHostingEnvironment env)
        {
            // Get json request data
            string requestBody = Request.ReadBody();
            ResponseData responseData = null;
            try
            {
                // And handle our request
                responseData = rhManager.HandleRequest<ResponseData>(requestBody, new ServiceVars(env.WebRootPath));
            }
            catch (Exception ex)
            {
                // log errors
                return BadRequest(); // Or what you need
            }
            return Json(responseData);
        }
    }
```

As you can see, all you need is to get the object as a json string and pass it to the manager.

# Customizing

In addition to the basic settings, you can configure everything you need:

* [Add your own handlers for new request types](#soon)

* [Override existing handlers with your implementations](#soon)

## Authors

* [f14shm4n](https://github.com/f14shm4n)

## License

[MIT](https://opensource.org/licenses/MIT)
