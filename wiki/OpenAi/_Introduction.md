# OpenAI Development

The purpose of this section is to outline the steps required to build an integration with OpenAI's GPT-x API.

It wraps the `Azure.AI.OpenAI.OpenAIClient` and provides opinionated methods for performing standard operations. These include:

- Creating `Embeddings` from text inputs.
- Saving `Embeddings` to a vector database.
- Retrieving `Embeddings` from a vector database.
- Performing `Similarity` operations on `Embeddings`.
- Performing `Search` operations on `Embeddings`.
- Performing `Classification` operations on `Embeddings`.