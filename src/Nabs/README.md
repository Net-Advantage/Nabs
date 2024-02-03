# Nabs Core Library

> WARNING: This library is still in development and is not yet ready for use.

Contains some key classes and interfaces that are used across the Nabs framework.

## Interfaces

- IDto - Used to mark an entity that represent a DTO (Data Transfer Object) in the application.
- ITenantEntity - Used to mark the entity that represent a tenant in a multi-tenant application.

### Guidance for DTOs

> Background: The Nabs Framework is predicated on the use of Entity Framework Core as the ORM.

DTOs are types that are exposed outside of the application. Our option is that you should not, as far as possible, expose Entities directly outside of the application.

## Enums

- TenantIsolationStrategy - Represents the strategy used to switch the behaviour multi-tenant application in dealing with application and data isolation.

## Classes

- ValueObject - Base class for value objects.