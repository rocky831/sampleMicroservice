<div id="top"> 
<h1 align="center">.Net 6 Microservice Deploy Instructions</h3>


This document covers the following subjects:
•	Install the .NET Core Hosting Bundle on Windows Server.
•	Create an IIS site in IIS Manager.
•	Deploy an ASP.NET Core app.

  

<!-- GETTING STARTED -->
## Prerequisites

•	.NET Core SDK installed on the development machine.
•	Windows Server configured with the Web Server (IIS) server role. If your server isn't configured to host websites with IIS, follow the guidance in the IIS configuration section of the Host ASP.NET Core on Windows with IIS article and then return to this tutorial.


### Install the .NET Core Hosting Bundle

Install the .NET Core Hosting Bundle on the IIS server. The bundle installs the .NET Core Runtime, .NET Core Library, and the ASP.NET Core Module. The module allows ASP.NET Core apps to run behind IIS.
Download the installer using the following link:
Current .NET Core Hosting Bundle installer (direct download)
1.	Run the installer on the IIS server.
2.	Restart the server or execute net stop was /y followed by net start w3svc in a command shell.

### Create the IIS site: 
1.	On the IIS server, create a folder to contain the app's published folders and files. In a following step, the folder's path is provided to IIS as the physical path to the app. For more information on an app's deployment folder and file layout, see ASP.NET Core directory structure.

2.	In IIS Manager, open the server's node in the Connections panel. Right-click the Sites folder. Select Add Website from the contextual menu.

1.	Provide a Site name and set the Physical path to the app's deployment folder that you created. Provide the Binding configuration and create the website by selecting OK.
 
1.	If the default identity of the app pool (Process Model > Identity) is changed from ApplicationPoolIdentity to another identity, verify that the new identity has the required permissions to access the app's folder, database, and other required resources. For example, the app pool requires read and write access to folders where the app reads and writes files.

### Get Artifact from the Build Pipeline: 
The artifact generated out of the build pipeline will be a zip file, download the file.

### Deploy the app
Move the contents of the bin/Release/{TARGET FRAMEWORK}/publish folder to the IIS site folder on the server, which is the site's Physical path in IIS Manager.
  
  
<h1 align="center">Instructions to Create New Microservice </h3>

### Install Microservice Template into dotnet env: 

Clone https://dev.azure.com/AVEVA-VSTS/_git/Predictive%20Analytics?path=/MicroserviceTemplate
Go to MicroserviceTemplate folder
                \...\PredictiveAnalytics\MicroserviceTemplate\AVEVA.PA.MicroserviceTemplate

Run command 
```
dotnet new --install . 
```

Once installed,  
```
dotnet new --list should show 'PA-Microservice' template installed.
```


### Create new Microservice out of the Template     
Go to location , where you want to create microservice.  Per guideline all microservices should be created under \...\PredictiveAnalytics\Microservices
And run the command: 
```
                dotnet new PA-Microservice -o {{newMicroserviceName}}
                e.g. dotnet new PA-Microservice -o AssetService
```
This will create new Microservice solution, port all the sample code that is built in the template, with namespace adjusted. 
This should have all biloerplate code to create an API, with Swagger, Message publisher, Message Consumer, UOW.. etc




<h1 align="center"> Standards, Conventions and Practices </h3>

### Clean Architecture Code organization :
##### Core
This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

##### Application
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

##### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

##### Web
This layer contains the API controllers. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only Startup.cs should reference Infrastructure.


### C# Coding Conventions :
https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions


### Microservice Canvas :

_Concept, the **Microservice Design Canvas** intends to capture the essential service attributes 
that can help guide development of the service itself as well as its consuming applications._

https://dzone.com/articles/streamlined-microservice-design-in-practice
