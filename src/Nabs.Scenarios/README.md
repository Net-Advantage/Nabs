# Nabs Scenarios Library

This package contains a set of contracts that are essential for propagating application-wide state.

- TenantIsolationStrategy - Indicates the tenant isolation applied to the application.
- IApplicationContext - Provides access to the application context.
- ITenantContext - Provides access to the tenant context.
- ITenantEntity - Provides access to the tenant entity. All EFCore Entity types that need to be identified as tenant-specific should implement this interface.

Some common scenarios are supported:
- List Items
- Get Item
- Create Item
- Update Item
- Delete Item
- Soft Delete Item
- Restore Item

## CQRS Support with MediatR:

It takes a dependency on MediatR with abstractions to assist with some common scenarios such as:

In addition to the the command and query segregation, it also provides the ability to segregate I/O and business logic.
The abstractions provide a way to chain I/O operations and business logic (BL) operations in a pipeline.

E.g.:

List Items => Load Data (I/O) -> Mapping Data (BL) -> Return Data

Get Item => Load Data (I/O) -> Validate Data (BL) -> Return Data

Create Item => Validate Request (BL) => Load Data (I/O) -> Validate Data (BL) -> Save Data (I/O) -> Return Data


The SQL Save Data will do all the SQL related exception handling.

## Orleans Grains Support:

