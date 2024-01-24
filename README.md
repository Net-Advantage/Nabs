# What is Nabs?
Net Advantage Business Solutions Core Libraries.

These libraries are made available to customers of Net Advantage Business Solutions and form part our our consulting services.

We believe that software development teams' primary objective is to address business needs. In order to do this certain technical 
The primary pupose is to provide a set of opinionated libraries to development teams in order to get them solving business challenges rather than working out how to approach aspects of their technical projects.

[Nabs Code Coverage](https://net-advantage.github.io/Nabs/coverage)

## In this repository

This repository is mostly compose of Services, Abstractions and Extensions to help making the following easier:

- Nabs - Extensions for working with Reflection, Primitives, Etc.
- Nabs.Tests - supports xUnit testing.
- Nabs.Secrets - Abstractions for secret management.
- Nabs.Persistence - Abstractions for persistence operations.
- Nabs.Identity - Abstractions for Authentication and Authorisation.
- Nabs.Monitoring - Abstractions for Logging, Metrics and Alerting.
- Nabs.Configurations - Abstractions for configuration and secret management.

## SOLID Principles

The general approach to these libraries is that they are to follow the SOLID principles - at least as I see them implemented.

### Single Responsibility Principle

### Open / Close Principle

Each library will provide sensible extension points for developers to hook up functionality and extend capability.

### Liskov Substitution Principle

Each library will provide contracts by way of interfaces and abstractions to unsure common programming paradigms regardless of the implementation. 

### Interface Segregation Principle

Each library will attempt to provide granular interfaces to ensure limited API surface area.

### Dependency Inversions Principle

Each library will contain a static extension method class called `DependencyInversionExtensions`. These extensions methods will allow you to register the types with the `IServiceCollection`.

## Getting started

Once you have installed the Nuget package, you should be able to get started with Unit Tests, Persistence, Identity and Monitoring by adding the dependencies and configuring.

At design time and run time you will receive warnings, errors and/or exceptions if anything is misconfigured. This will allow you to fix them and move forward.
