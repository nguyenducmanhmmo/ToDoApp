resource "azurerm_resource_group" "todoapp" {
  name     = var.resource_group_name
  location = var.location

  tags = {
    Scope = "Todoapp"
  }
}
