# What is Nabs?
Net Advantage Business Solutions Core Libraries.

These libraries are made available to customers of Net Advantage Business Solutions and form part our our consulting services.

We believe that software development teams' primary objective is to address business needs. In order to do this certain technical 
The primary pupose is to provide a set of opinionated libraries to development teams in order to get them solving business challenges rather than working out how to approach aspects of their technical projects.

## In this repository

This repository is mostly compose of Services, Abstractions and Extensions to help making the following easier:

- Nabs - Extensions for working with Reflection, Primitives, Etc.
- Nabs.Tests - supports xUnit testing.
- Nabs.Persistence - Abstractions for persistence operations.
- Nabs.Identity - Abstractions for Authentication and Authorisation.
- Nabs.Monitoring - Abstractions for Logging, Metrics and Alerting.
- Nabs.Configurations - Abstractions for configuration and secret management.

## Genral Principles

The general approach to these libraries is that they are to follow the *Dependency Inversions Principle* - at least as I see it.

Each library will contain an extension method class called `DependencyInversionExtensions`. These extensions methods will allow you to register the types with the `IServiceCollection`.

I have tried as far as possible to follow the *Open/Close Principle*.

## Getting started

Once you have installed the Nuget package, you should be able to get started with Unit Tests, Persistence, Identity and Monitoring by adding the dependencies and configuring.

At design time and run time you will receive warnings, errors and/or exceptions if anything is misconfigured. This will allow you to fix them and move forward.
