# Object Builder

The purpose of object builder is to provide the ability for business people to:

- Define business objects in terms that suite their domain.
- Define properties on those business objects.
- Define rules to validate the business objects.
- Define rules to mutate the business objects.
- Define rules to transform the business objects.

## Anatomy of a business object

The namespace is: `Nabs.ObjectBuilder`

- IObject
    - Provides the structure of the business object.
- IObjectProperty
    - Provides the details of the structure of the business object.
- IDto
    - Provides the structure of the business object that is exposed outside of the internal system.
- IObjectValidator
- IObjectMutator
    - Provides rules used to mutate properties (IObjectProperty) of the IObject
- IObjectContext
    - Provides a context that aggregates a collection of business objects
- IObjectContextEnricher
    - Mutates a object context
- IObjectMapper
    - Provides outputs from the object context.
