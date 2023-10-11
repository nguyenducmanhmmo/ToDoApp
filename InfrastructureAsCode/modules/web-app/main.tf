resource "azurerm_service_plan" "todoapp" {
  name                = "webappserviceplan${var.random}"
  location            = var.location
  resource_group_name = var.resource_group_name
  os_type             = var.azurerm_service_plan_os_type
  sku_name            = var.azurerm_service_plan_sku

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_windows_web_app" "todoapp_api" {
  name                = var.webapp_api_project_name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.todoapp.id

  site_config {
    always_on         = false
    app_command_line  = "dotnet TodoApp.Api.dll"
    use_32_bit_worker = true
    application_stack {
      dotnet_version = var.dotnet_version
    }
  }

  app_settings = {
    "WEBSITES_PORT" = "5000"
  }

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_windows_web_app" "todoapp" {
  name                = var.webapp_ui_project_name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.todoapp.id

  site_config {
    always_on = false
  }

  app_settings = {
    "WEBSITES_PORT"                = "80"
    "WEBSITE_NODE_DEFAULT_VERSION" = var.node_version
  }

  tags = {
    Scope = "Todoapp"
  }
}