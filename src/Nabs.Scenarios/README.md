# Nabs Scenarios Library

This package contains a set of contracts that are essential for propagating application-wide state.

- TenantIsolationStrategy - Indicates the tenant isolation applied to the application.
- IApplicationContext - Provides access to the application context.
- ITenantContext - Provides access to the tenant context.
- ITenantEntity - Provides access to the tenant entity. All EFCore Entity types that need to be identified as tenant-specific should implement this interface.
