output "resource_name_suffix" {
  description = "Suffix of the name of every resource"
  value       = random_string.todoapp.result
}

output "sql_server_fqdn" {
  value = azurerm_mssql_server.todoapp.fully_qualified_domain_name
}

output "sql_database_id" {
  value = azurerm_mssql_database.todoapp.id
}

output "key_vault_uri" {
  value = azurerm_key_vault.todoapp.vault_uri
}

output "api_url" {
  value = "https://${azurerm_windows_web_app.todoapp_api.name}.azurewebsites.net/api"
}

output "angular_url" {
  value = "https://${azurerm_windows_web_app.todoapp.name}.azurewebsites.net"
}