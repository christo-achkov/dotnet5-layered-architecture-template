# .NET Core layered architecture tempalte
A layared architecture template for .NET Core. Can easily be fitted into WEB API or MVC project.

![layered architecute.png](./layered-architecture.png)

## Why use repository with entity framework?
While we have all heard the argument about entity framework already being a repository thus we should not use repositories I think his argument is highly flawed and misses the key reason why would one want to use a repository.
Are you really okay with being hard coupled to something microsoft likes to change completely every year?
Are you really okay with writing the same query over and over again for something more complex?
Are you really okay with the built in unit testability of EntityFrameworkCore? While it has gotten better since 2.0 it is still annoying to test in many cases. Even if we ignore all of the above there are still many possitives which having a repository layer with EF provides. Even in the official MSDN, microsoft provides a lot of examples of repository pattern using EF Core (https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).  If you would like to get into the nitty girtty I would recommend reading this stackoverflow comment (https://stackoverflow.com/questions/13180501/what-specific-issue-does-the-repository-pattern-solve/13189143#13189143)
