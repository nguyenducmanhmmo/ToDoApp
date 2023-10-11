resource "azurerm_mssql_server" "todoapp" {
  name                         = "sqlserver${var.random}"
  resource_group_name          = var.resource_group_name
  location                     = var.location
  version                      = var.sqlserver_version
  administrator_login          = var.sqlserver_administrator_login
  administrator_login_password = var.db_password

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_mssql_firewall_rule" "todoapp" {
  name             = "sqlserverfwrule${var.random}"
  server_id        = azurerm_mssql_server.todoapp.id
  start_ip_address = var.azurerm_mssql_firewall_rule_start_ip_address
  end_ip_address   = var.azurerm_mssql_firewall_rule_end_ip_address
}

resource "azurerm_mssql_database" "todoapp" {
  name      = var.sqlserver_db_name
  server_id = azurerm_mssql_server.todoapp.id
  collation = var.azurerm_mssql_database_collation
  sku_name  = var.azurerm_mssql_database_sku
  tags = {
    Scope = "Todoapp"
  }
}