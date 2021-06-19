# .NET Core layered architecture tempalte
A layared architecture template for .NET Core. Can easily be fitted into WEB API or MVC project.

![layered architecute.png](./layered-architecture.png)

## Why no validation?
The way you do validation can be altered a lot. For example in MVC the recommended way to do its via model attributes, while in WEB API you can have a custom object where you set flags. In the latter case all you have to do is add another service decorator with validator for each model that sets flags depending on the command/query state.
## Why no users or authorization?
This is another highly specific thing. In some caseds you might not even need users. Also some people prefer having a custom built authentication. So I have decided to omit it.
## Why no tracing?
There are just too many ways to do tracing depending on your scenario. I have decided to omit it for now.
## Why use repository with entity framework?
While we have all heard the argument about entity framework already being a repository thus we should not use repositories I think his argument is highly flawed and misses the key reason why would one want to use a repository.
Are you really okay with being hard coupled to something microsoft likes to change completely every year?
Are you really okay with writing the same query over and over again for something more complex?
Are you really okay with the built in unit testability of EntityFrameworkCore? While it has gotten better since 2.0 it is still annoying to test in many cases. Even if we ignore all of the above there are still many possitives which having a repository layer with EF provides. Even in the official MSDN, microsoft provides a lot of examples of repository pattern using EF Core (https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).  If you would like to get into the nitty girtty I would recommend reading this stackoverflow comment (https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
