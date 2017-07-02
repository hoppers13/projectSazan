ProjectSazan.Persistence.FileSystem should be a project in its own.
Unfortunately so far I've not found a way to reference Microsoft.AspNetCore.Http.Features outside of the web (MVC) project.
So for the moment this is the best solution - with a view to come back and spent more time to find a way of extracting the code contained in this folder to its own VS project 
(and therefore, ultimately, to its own assembly)