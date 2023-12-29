# NABS - Net Advantage Business Solutions

This repository contains the source code that I use when building line of business application for customers. It is a collection of libraries, tools, and templates that I have found useful over the years. The help me be more productive.

It is important to notice that these libraries are very granular. They follow a type of single responsibility principle. For me it is important that I can pick and choose the libraries that I need for a specific piece of functionlity.

For example, I have have a Nuget package that only contains abstractions for Linq to objects. I then have another package that only contains abstractions for Linq for EFCore.

There are times I want to build a feature that only uses Linq to objects. I can then use the Linq to objects abstractions. There are other times I want to build a feature that only uses Linq to EFCore. I can then use the Linq to EFCore abstractions.

