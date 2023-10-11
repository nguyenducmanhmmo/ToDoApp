# Introduction

This project consists of a ToDo Web API created with the .NET Cor 7 And Angular 13 that is connected to an [Azure SQL Database], [Azure Key Vault] and deployed to an [Azure Web App] using [Terraform].


The project infrastructure is show in the next picture:

![Architecture](./pictures/Architecture.png?raw=true "Architecture")


# How to use it

## 1. Setting up your Azure account

Install Azure Cli, you need it to run Terraform CLI and create new subscription. After that, login and set current Subscription

```shell
az login

az account set --subscription [subscription name or id]

az ad sp create-for-rbac --name "[service principal name]" --role contributor --scopes /subscriptions/[subscription id]
```

## 2. Creating the Terraform backend in Azure

Create a Resource Group in your Subscription and a Storage Account inside of it, that will be used by the Terraform client as the backend.
You can do it by running the next scripts :

```shell
az group create --name "[resource group name] --location "[location]"
  
az storage account create --resource-group [resource group name] --name [storage account name] --location [location] --sku Standard_LRS --encryption-services blob

$accountKey=$(az storage account keys list --account-name "[storage account name]" --query "[?permissions == 'FULL'].[value][0]" --output tsv)

az storage container create --name "[container name]" --account-name "[storage account name]

$storage_account_key=(az storage account keys list --resource-group [resource group name] --account-name [storage account name] --query '[0].value' -o tsv)
```

## 3. Set it up with terraform backend
Make sure you install Terraform CLI in your machine

```terraform
backend "azurerm" {
    resource_group_name  = "[resource group name used for the Terraform backend]"
    storage_account_name = "[storage account name used for the Terraform backend]"
    container_name       = "[storage container name used for the Terraform backend]"
    key                  = "terraform.tfstate"
  }
```

## 4.Should have the following things created:

**Azure:**
- A dedicated Subscription for all the Resources.
- A dedicated Resource Group inside the Subscription, for the Terraform backend.
- A Storage Account inside the Resource Group, to be used by the Terraform client.
- A Blob Container inside the Storage Account, to store the Terraform client state.
- A Service Principal in your Tenant (optional)
- The **backend** section of the */InfrastructureAsCode/main.tf* Terraform file updated with the settings of the Storage Account used for the Terraform backend.

## 5.Create Resources with Terraform CLI

```shell
# Init
terraform init
# Check 
terraform fmt -check
# Create a plan
terraform plan -out tfplan -input=false
# Apply terraform
terraform apply -input=false tfplan
```


# Todo Project - ASP.NET Core 7 - Angular 13

This template is for a clean structured ASP.NET Core and Angular for Todo Project project.

## 1. Project Structure

The project structure is designed to promote separation of concerns and modularity, making it easier to understand, test, and maintain the application.

```
├── TodoApp.Api
│   ├── Core                    # Contains the core business logic, domain models, view models,etc.
│   ├── Infrastructure          # Contains infrastructure concerns such as data access, external services, etc.
│   ├── API                     # Contains the api, including controllers, extensions, etc.
│   └── UnitTest                # Contains unit tests for the core, api and infrastructure layer
├── TodoApp
│   ├── helpers                 # Contains guard, intercepters ....
│   ├── private                 # Contains Private module (core business ...)
│   ├── public                  # Contains Public module (login, register...)
│   └── shared                  # Contains models, serives
```
## 2. Database Migration
Run next srcipts when done changing  database connection string in the `appsettings.json`

```shell
dotnet ef migrations add initialmigration --project TodoApp.Infrastructure/TodoApp.Infrastructure.csproj --startup-project TodoApp.Api/TodoApp.Api.csproj

dotnet ef database update --verbose --project TodoApp.Infrastructure/TodoApp.Infrastructure.csproj --startup-project TodoApp.Api/TodoApp.Api.csproj
```

## 3. Build Local

To use this project template, follow the steps below:

1. Ensure the .NET 7 SDK and Node 16 is installed on your machine.
2. Open the solution in your preferred IDE (e.g., Visual Studio, Visual Studio Code).
3. Build the solution to restore NuGet packages and compile the code.
4. Configure the necessary database connection settings in the `appsettings.json` file of the API project.
5. Run .Net Api as start up project
6. restore npm packages in angular UI
7. Run Angular by command


## 4. FrontEnd
After done with backend, start angular project by below scripts
```shell
npm install

ng serve
```

## 5. KeyVaults
If use want to use azure key vaults add key vault configuration inside appsetting.json and add access policy for the application

![KeyVaults](./pictures/key_vault_access_policies.png?raw=true "KeyVaults")


## 6. Output
And here's how it looks like

Login Screen try user01/user01 or user02/user02

![LoginScreen](./pictures/Login_Screen.png?raw=true "LoginScreen")

Todo Screen
![TodoScreen](./pictures/Todo_Screen.png?raw=true "TodoScreen")

## 7. UnitTest
Currently, this project only write unit test for Backend

![UnitTest](./pictures//UnitTest.png?raw=true "UnitTest")

