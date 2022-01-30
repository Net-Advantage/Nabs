# Nabs.Persistence

Abstractions, Services, Extensions and Data Types to persist stuff.

There are a common set of operations for querying things from and saving things to some type of data store.

In today's world, most business challenges are best solved by taking a hybrid approach to data storage.

Our opinion is that software development teams need to use multiple forms of storage to best address the business challenges being presented.

The `Nabs.Persistence` libraries provide a common interface for storing various forms of data.

## Supported Persistence

- Relational data using SqlServer, Sqlite or CosmosDb.
- Tabular data using Azure Table Storage.
- Files and blobs using Azure Blob Storage.

 We use Entity Framework (EFCore6) under the covers for relational data.

## Extensibility

Teams can also extend our persistence layer to introduce custom stores not supported by `Nabs` so that teams can work with data in a consisten way across all data stores in the organisation.

If for example, your organisation requires `PostreSQL`, your team will be able to extend our core libraries with the `Npgsql.EntityFrameworkCore.PostresSQL` provider with relatively little effort in order to achieve this consistence across data stores. The same goes for `MySql`.
