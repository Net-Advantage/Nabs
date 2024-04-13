# Nabs OpenAI Library

> WARNING: This library is still in development and is not yet ready for use.

An opinionated version with OpenAI APIs.

## What this scenarios does this library address?

AI can be described as a set of task that are performed by a machine are are typically performed by humans.

This api is an attempt to solve a specific interaction between our users and the OpenAI API.

At this stage my understanding is the there are some core parts that need to be fulfilled:
- Data that describes the context of the conversation.
- A model that can be used to interpret the data and generate a response.
- A way to manage the interactions between the user and the model such that they can be put together in a way that makes sense.

### The Context

We will attempt to create a context that describes the area of specialization of the System. This includes:

- A description of the area of specialization.
- A description of the organization behind the area of specialization.
- The specific approach this organisation has to the area of specialization.
- Terms that define the meaning of key concepts in the area of specialisation.
- The personas representing the organization who will be responsible for responses.
- The content that will be used to inform the generation of responses.
- The policies that govern the way responses are generated.
- The characteristics of the users interacting with the system.

### The Model
For this purpose we have selected OpenAI's GPT-4 model.
Because the context is quite specific, we will need to fine-tune the model to limit the scope of the responses to the context.
We will not be advocating for custom models that require a lot of resources to train.

### The Interaction
The interaction need to make sense and also be cost effective.
There are two aspects to this interaction:
- Creating and managing embeddings
- Keeping track of the conversations



