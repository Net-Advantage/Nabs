# Nabs Activity Framework Library

> WARNING: This library is still in development and is not yet ready for use.

The Nabs Activity Framework is base of an opinion that all work done by organisations can be
distilled down to a series of activities. These activities, in turn, can be broken down into a series of
behaviours - the procedures. The activities are then assembled into workflows.
Some activities are simple and can be found repeated across many workflows.
Sometimes it is necessary to group workflows into sub-domains.

Here is a simple hierarchy of the framework:

```
Sub-domain
|  Workflow
+--  Activity
   +--  Behaviour
```

## Workflow
Workflows are responsible for the following:
- Defining the activities that make up the workflow.
- Holding the workflow state.
- Initialising the workflow state.
- Orchestrating the execution of the activities.
- Persisting the workflow state.
- Raising events.

## Activities
Activities are responsible for the following:
- Defining the behaviours that make up the activity.
- Holding the activity state.
- Initialising the activity state.
- Orchestrating the execution of the behaviours.
- Enriching the workflow state.

## Behaviours
Behaviours are responsible for the following:
- Collecting information from other activities.
- Transforming the activity state.
- Validating the activity state.
- Projecting information.
- Raising events.

## Sub-domains
Sub-domains are responsible for the following:
- Grouping workflows.

## Developer Productivity
The framework is designed to increase developer productivity by:
- Providing a consistent way of defining workflows, activities and behaviours across the organisation.
- Providing a consistent way of modelling the state of workflows, activities and behaviours across the organisation.
- Ensuring that there is a clear separation of concerns between data loading/persistence and the business logic.

When the Nabs Activity Framework is combined with the Nabs Activity Sculpting Methodology, organisations will be able to:
- Create a unified language for describing the work that they do.
- Provide clarity on the details.
- Identify commonalities and reduce duplication.
- Adopt a structured approach to the delivery of features and projects.
- Onboard new staff more quickly.

## Getting Started
Use the following steps to get started with the Nabs Activity Framework:
- Define your own Activity Sculpting Methodology implementation template.
	- Identify the `Activity` you will be modelling and give it a name. E.g. Shopper Registration Activity
	- Identify the structure of the `Activity State` you are modelling (Properties).
	- Identify the `Behaviours` that make up each `Activity` (Transform, Validate, Project, Events).
	- Identify the data sources and destinations that are required for the `Activity`.
	- Identify the `Events` that are raised by each `Behaviour`.

Here is an example of an Activity Sculpting Methodology implementation template:

```
Activity: Shopper Registration Activity
Activity State:
- ShopperId
- ShopperName
- ShopperEmail
- ShopperPassword
- ShopperDateOfBirth
- ShopperAddress
- ShopperPhoneNumber
Activity Behaviours:
- Shopper Registration Initialisation Behaviour

```

