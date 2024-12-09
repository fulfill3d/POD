# POD

- **App**: Visit https://pod.fulfill3d.com to see the application demo.

An enterprise level cloud-based ecommerce app with 3D print-on-demand business model having full integration capabilities to Shopify stores with Braintree and Stripe payment options.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Microservices](#microservices)
- [Frontend Tech Stack](#frontend-tech-stack)
- [Backend Tech Stack](#backend-tech-stack)

## Prerequisites

Before running the project, ensure you have the following installed:

- **Node.js** (version 22 or higher recommended)
- **npm** or **yarn** (package manager)

## Project Structure

Here is an overview of the key directories in the project:

- **`src/app`**: Contains Next.js pages and components for the application, including routing and main UI layout.
- **`src/components`**: Reusable React components used throughout the app, such as form elements, buttons, modals, etc.
- **`src/hooks`**: Custom hooks for managing application state, fetching data, and interacting with external APIs.
- **`src/models`**: TypeScript interfaces and types representing the data models used across the application.
- **`src/msal`**: Contains MSAL (Microsoft Authentication Library) related configurations, components, custom hooks, and wrappers.
- **`src/service`**: Service files for connecting to APIs, handling data requests, and managing authentication (e.g., MSAL).
- **`src/store`**: Redux store configuration and slices for state management.
- **`src/svg`**: SVG files used for icons and other vector-based elements in the UI.
- **`src/types`**: Global types.
- **`src/utils`**: Utility functions and helper methods used across different parts of the project to perform common tasks like formatting, validation, etc.

## Microservices

### Admin API
- **POD.API.Admin.Filament:** ...
- **POD.API.Admin.Model:** ...

### Seller API
- **POD.API.Seller.Address:** ...
- **POD.API.Seller.Payment:** ...
- **POD.API.Seller.Product:** ...
- **POD.API.Seller.Store:** ...

### User API
- **POD.API.User.Identity:** ...

### Functions
- **POD.Functions.Geometry:** ...
- **POD.Functions.Payment.Schedule:** ...
- **POD.Functions.Payment.Processing.Braintree:** ...
- **POD.Functions.Payment.Processing.Stripe:** ...
- **POD.Functions.Payment.PostProcessing:** ...
- **POD.Functions.Payment.PublishSchedule:** ...
- **POD.Functions.Shopify.AppInstall:** ...
- **POD.Functions.Shopify.AppUninstall:** ...
- **POD.Functions.Shopify.CallExecutes:** ...
- **POD.Functions.Shopify.CreateWebhooks:** ...
- **POD.Functions.Shopify.DeleteProduct:** ...
- **POD.Functions.Shopify.OrderProcessing:** ...
- **POD.Functions.Shopify.PublishProcessing:** ...
- **POD.Functions.Shopify.UpdateInventory:** ...
- **POD.Functions.Shopify.WebhookEndpoints:** ...

## Frontend Tech Stack

- **Next.js (14.2.15)**: A React framework with server-side rendering and static site generation.
- **React (18)**: A JavaScript library for building user interfaces.
- **TypeScript (5.6.3)**: A statically typed superset of JavaScript.
- **Tailwind CSS (3.4.13)**: A utility-first CSS framework for building responsive UIs.
- **MSAL Browser (3.26.1)**: Microsoft Authentication Library for handling authentication in browser-based applications.
- **MSAL React (2.1.1)**: A library for integrating MSAL with React applications.
- **Redux Toolkit (2.2.8)**: A toolkit for efficient Redux development.
- **Leaflet (1.9.4)**: An open-source JavaScript library for interactive maps.
- **React Redux (9.1.2)**: Official React bindings for Redux.
- **React Leaflet (4.2.1)**: React components for Leaflet maps.
- **Stripe.js (2.1.11)**: Stripe JavaScript SDK for handling Stripe payment integrations.
- **React Stripe.js (2.3.1)**: React components for Stripe payments.
- **Braintree Web (3.97.3)**: A JavaScript SDK for Braintree payment processing.
- **ESLint**: A linter to ensure code quality and consistency.

## Backend Tech Stack

- **Backend:** .NET 8 (Isolated Worker), Azure Functions v4
- **Messaging:** Azure Service Bus
- **Storage:** Azure Blob Storage
- **Database:** Azure SQL Database
- **Database Migration:** Fluent Migrator with version control
- **Authentication:** Azure AD B2C
- **Configuration & Secrets Management:** Azure App Configuration, Azure Key Vault
- **Hosting:** Microsoft Azure
- **External APIs:** Braintree, Stripe, Shopify
