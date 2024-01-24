<a href="https://net-advantage.github.io/Nabs/coverage/" target="_blank"><img src="https://img.shields.io/badge/Code-Coverage-brightgreen" alt="Coverage"></a>

# What is Nabs?
Net Advantage Business Solutions Core Libraries.

These libraries are made available to customers of Net Advantage Business Solutions and form part our our consulting services.

We believe that software development teams' primary objective is to solve address business requirements. Some form of work is required to solve technical challenges, but this should be minimised.

The primary purpose of this repo is to provide a set of opinionated libraries to development teams in order to abstract away the technical aspects that do not contribute directly to addressing the business requirements.

## In this repository

This repository is mostly compose of Services, Abstractions and Extensions to help making the following easier:

- Nabs - contracts used across the application.
- Nabs.ActivityFramework - abstractions to supports the Activity Framework.
- Nabs.AzureConfiguration - WIP - abstractions to supports Azure Configuration.
- Nabs.Identity.Web - abstractions to supports internal and external identity.
- Nabs.Linq - abstractions to supports LINQ.
- Nabs.Persistence - abstractions for persistence operations.
- Nabs.Reflection - abstractions to supports reflection.
- Nabs.Resources - abstractions to supports reading and processing embedded resources.
- Nabs.Scenarios - abstractions to supports business application scenarios.
- Nabs.Secrets - abstractions for secret management.
- Nabs.Serialisation - abstractions to supports serialisation.
- Nabs.Tests - abstractions to supports xUnit testing.
- Nabs.Tests.DatabaseTests - abstractions to supports xUnit testing with a database.

> All of these packages are a work in progress and will continue to change as we learn more about the best way to implement them.

## SOLID Principles

The general approach to these libraries is that they are to follow the SOLID principles - at least as I see them implemented.

