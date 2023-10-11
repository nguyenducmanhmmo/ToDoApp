variable "location" {
  description = "Location of the resources"
  type        = string
  sensitive   = false
  default     = "southeastasia"
  nullable    = false
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
  sensitive   = false
  default     = "todoapp-resource-group"
}

variable "sqlserver_name" {
  description = "Username of the Azure SQL Server"
  type        = string
  sensitive   = true
  nullable    = false
  default     = "todoapp-sqlserver-orient-test"
}

variable "sqlserver_db_name" {
  description = "Name of the Azure SQL Database"
  type        = string
  sensitive   = true
  nullable    = false
  default     = "todoapp-orient-test"
}

variable "sqlserver_administrator_login" {
  description = "Username of the Azure SQL Instance"
  type        = string
  sensitive   = true
  nullable    = false
  default     = "orient-test"
}

variable "sqlserver_administrator_login_password" {
  description = "Password of the Azure SQL Instance"
  type        = string
  sensitive   = true
  nullable    = false
  default     = "1VgMDxt3AmoPAWZ"
}

variable "keyvault_name" {
  description = "Name of the Azure Key Vault"
  type        = string
  default     = "orient-test"
}

variable "webapp_api_project_name" {
  description = "Name of the .NET Api Project that has to be run"
  type        = string
  nullable    = false
  default     = "orient-test-todo-app-api"
}

variable "webapp_ui_project_name" {
  description = "Name of the Angular Project that has to be run"
  type        = string
  nullable    = false
  default     = "orient-test-todo-app"
}

variable "storage_account_name" {
  description = "Name of the Azure Storage Account for Terraform state"
  type        = string
  default     = "terraformbackendstactest"
}

variable "tenant_id" {
  description = "Name of the Azure Storage Account for Terraform state"
  type        = string
  default     = "e7d7e194-6b30-4737-bc76-8a2900fd2675"
}

variable "service-principal-object-id" {
  description = "Name of the Azure Storage Account for Terraform state"
  type        = string
  default     = "bb4485cb-5b7b-4c81-bde5-0a7c66b893b1"
}