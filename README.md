# Azure Sentinel PowerShell Module

## ⚠ Please Read This Before Using

- **This module is still a work in progress. Documentation is not fully created and unknown errors may occur while using.**
- **In addition, this module:**
    - Is not an official Microsoft project and Microsoft may create their own PowerShell module in the future.
    - This project's implementation may be completely reworked. This module **is not production ready**.

## Install

### Building from source

#### Prerequisites

- .NET Core 3.0 SDK

#### Build

1. Launch a PowerShell prompt.
2. Set the current directory to the project directory.
3. Run `./build.ps1` in the directory.
4. The module is then built in the `pwsh-azuresentinel` folder. Copy that folder to your PowerShell modules folder.

## Setup

### Azure AD App Registration

1. Log into your Azure AD portal and navigate to **App Registrations**.
2. Click on **New registration**.
3. On the **Register an application page**...
   1. Name the app whatever you want to name it.
   2. Leave the **Supported account types** as *Accounts in this organizational directory only (Your Tenant Name only - Single tenant)*.
   3. Set **Platform configuration (Optional)** to *Client Application (Web, iOS, Android, Desktop+Devices)*.
   4. Click **Register** when finished.
4. When redirected to the app's page, navigate to the **Authentication** page for the app.
   1. Click **Add a platform**.
   2. Choose **Mobile and desktop applications**.
   3. And add the suggested redirect URI `https://login.microsoftonline.com/common/oauth2/nativeclient`.
   4. Click **Configure**.
   5. Under **Advanced settings** change the option for ***Treat application as a public client*** to *Yes*.
   6. Click **Save** at the top of the page.
5. On the left side, click on **API permissions**.
    1. Click on **Add a permission** and then click on the **Azure Service Management**.
    2. Check the checkbox for **user_impersonation**.
    3. Click the **Add permissions** button.
    4. Scroll to the bottom of the screen and click on the **Grant admin consent for *Your Tenant Name*** button. Click on **Yes** when prompted.
6. Click on the **Overview** link on the left side of the screen of the app page and note the following for later:
    1. Application (client) ID
    2. Directory (tenant) ID

In addition, you will need three extra pieces of information:

1. The **Workspace Name** of the Log Analytics workspace for Azure Sentinel
2. The **Resource Group Name** where the workspace is located.
3. The **Subscription ID** for the tenant where the Azure Sentinel resources are located in your tenant.

### Configure the Azure Sentinel PowerShell module

After importing the `pwsh-azuresentinel` module, run the following command:

```powershell
Set-SentinelModuleConfig -PublicClientId "95155854-bb54-4533-a3e0-14af326e997f" -TenantId "5b6a210c-711e-476a-a99c-2460df178748" -ResourceGroupName "az-sentinel-rscgrp" -SubscriptionId "ffe85413-ce98-43e9-b610-2efd7c91470e" -WorkspaceName "az-sentinel"
```

- `-PublicClientId` is associated with the app registration's *Application (client) ID*.
- `-TenantId` is associated with your Azure AD's *Directory (tenant) ID*.
- `-ResourceGroupName` is the resource group your Azure Sentinel workspace is located.
- `-SubscriptionId` is associated with your Subscription ID that the Azure Sentinel workspace is.
- `-WorkspaceName` is associated with the name of your Azure Sentinel workspace.

*\* Module config is saved to the user profile directory under `.pwsh-azsentinel`.*

## Using the Module

To connect to Microsoft Graph, run the cmdlet:
```powershell
Connect-Sentinel
```

This will prompt the **Device Code Flow** with a code you must enter on the [Microsoft Device Logon](https://microsoft.com/devicelogin) page through a web browser. After authenticating, it will return the authentication token back to the prompt.

*\* The authentication token is saved to the current session, so there's no need to save the return to a variable.*


## Project Dependencies

- **MSAL for .NET**
  - GitHub - [Link](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet)
  - Nuget - [Link](https://www.nuget.org/packages/Microsoft.Identity.Client)
- **Newtonsoft.Json**
  - GitHub - [Link](https://github.com/JamesNK/Newtonsoft.Json)
  - Nuget - [Link](https://www.nuget.org/packages/Newtonsoft.Json/)
  - Website - [Link](https://www.newtonsoft.com/json)