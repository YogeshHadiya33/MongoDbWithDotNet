# MongoDB with .NET 

This solution demonstrates how to integrate MongoDB with a .NET application using the official MongoDB C# driver. It provides a basic framework for performing CRUD operations on MongoDB collections through a RESTful API. The solution is structured into several key files that set up the MongoDB connection, define the controllers for handling HTTP requests, and configure the application startup.

## Program.cs

`Program.cs` is the entry point of the application. It performs the initial setup, including configuring the MongoDB client, registering services, and setting up middleware. Key components include:

- **MongoDB Client Configuration**: Establishes a connection to MongoDB using connection settings defined in the application's configuration (`appsettings.json`). It specifies the connection string and SSL settings.
- **Service Registration**: Registers the MongoDB client as a singleton service, making it available for dependency injection throughout the application.
- **Middleware Configuration**: Sets up essential middleware for the application, including routing, Swagger for API documentation, and HTTPS redirection.


## MongoDatabaseController.cs

`MongoDatabaseController.cs` provides a controller for database-level operations, allowing management of collections within the MongoDB database:

- **CreateCollection**: Creates a new collection within the database.
- **DropCollection**: Deletes a collection from the database.
- **ListCollections**: Lists all collections in the database.
- **RenameCollection**: Renames an existing collection.
- **GetCollection**: Get collection to interact with stringly typed class.


## MongoCollectionController.cs

`MongoCollectionController.cs` defines a controller that handles operations on a specific MongoDB collection, in this case, an Employee collection. It demonstrates how to perform various CRUD operations:

- **Create**: Adds a new Employee document to the collection.
- **CreateMany**: Inserts multiple Employee documents into the collection at once.
- **GetAll**: Retrieves all documents from the Employee collection.
- **GetById**: Fetches a single Employee document by its ID.
- **Replace**: Replaces an existing Employee document with a new one.
- **UpdateField**: Updates a specific field in an Employee document.
- **Delete**: Removes an Employee document from the collection.
- **EstimatedDocumentCount**: Gets an estimated count of documents in the collection.
- **CountDocuments**: Counts documents in the collection based on a filter.
 

## MongoFiltersController.cs

`MongoFiltersController.cs` showcases how to perform various filter operations on a MongoDB collection, specifically targeting an Employee collection. It utilizes the MongoDB.Driver library to construct and execute queries based on different criteria. Here's a breakdown of the operations demonstrated:

- **Equal (`eq`)**: Retrieves documents where a specified field equals a given value. For example, finding employees with a specific name.
- **Not Equal (`ne`)**: Fetches documents where a specified field does not equal a given value.
- **Greater Than (`gt`)**: Selects documents where a specified field's value is greater than the provided value.
- **Greater Than or Equal (`gte`)**: Retrieves documents where a specified field's value is greater than or equal to the provided value.
- **Less Than (`lt`)**: Fetches documents where a specified field's value is less than the provided value.
- **Less Than or Equal (`lte`)**: Selects documents where a specified field's value is less than or equal to the provided value.
- **In (`in`)**: Retrieves documents where a specified field's value is in a given list of values.
- **Not In (`nin`)**: Fetches documents where a specified field's value is not in a given list of values.
- **And (`and`)**: Combines multiple query conditions with a logical AND, retrieving documents that match all conditions.
- **Or (`or`)**: Combines multiple query conditions with a logical OR, retrieving documents that match any of the conditions.
- **Exists (`exists`)**: Selects documents that have a specified field.
- **Type (`type`)**: Fetches documents where a specified field is of a given BSON type.
- **Regex (`regex`)**: Retrieves documents where a specified field matches a regular expression pattern.
- **All (`all`)**: Selects documents where an array field contains all the specified elements.
- **ElemMatch (`elemMatch`)**: Fetches documents where an array field contains at least one element that matches the specified query conditions.
- **Size (`size`)**: Retrieves documents where an array field is of a specified size.

Each of these operations is exposed through a dedicated endpoint, allowing for flexible querying capabilities based on different criteria. This controller serves as a practical guide for implementing and understanding MongoDB filter operations in a .NET application using the MongoDB.Driver library.

## Setup and Configuration

To run this solution, ensure you have MongoDB installed and accessible either locally or through a cloud provider. Configure the connection string and other relevant settings in `appsettings.json`. After configuring, run the application and use the provided Swagger UI to interact with the API endpoints.
 
