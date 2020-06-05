# Cargo Cult Engineering

I was randomly Googling TDD/Unit Testing best practices while avoiding productive work and I came across a question on the Software Engineering Stack Exchange where the OP asked if [there is such a thing as having too many unit tests?](https://softwareengineering.stackexchange.com/questions/348295/is-there-such-a-thing-as-having-too-many-unit-tests)

[This](https://softwareengineering.stackexchange.com/a/348300) was one of the top answers:

> If you have worked on large code bases created using Test Driven Development, you would already know there can be 
> such a thing as too many unittests. In some cases, most of the development effort consists of updating 
> low-quality tests that would be best implemented as invariant, precondition, and postcondition checks 
> in the relevant classes, at run-time (i.e. testing as a side effect of a higher level test).
>
> Another problem is the creation of poor quality designs, using cargo-cult driven design techniques, 
> that result in a proliferation of things to test (more classes, interfaces, etc). In this case the burden may seem to be updating the testing code, but the true problem is poor quality design.

[Cargo Cult Engineering](https://en.wikipedia.org/wiki/Cargo_cult_programming) is a new term to me, so I Googled that and read the Wikipedia article, which includes this line:

> The term cargo cult programmer may apply when an unskilled or novice computer programmer (or one inexperienced with the problem at hand) 
> copies some program code from one place to another with little understanding of how it works or whether it is required.

![picture alt](../images/awkward.JPG)


So yeah, I am aware that this describes me exactly at this stage in this project. I've carried over large chunks of [Jason Taylor's Northwind Traders Project](https://github.com/jasontaylordev/NorthwindTraders/tree/master/Src) into this project with basically no understanding of what some of these parts are doing. I've been fanatic about using XML Comments to document every public member in every class in the project, and it seems probable that some of these comments are superfluous. I also have 145 unit tests for just the Domain layer and the maybe 30% of the Application layer I've actually written.

## Argument for the Defense

Look, I know I don't know what I'm doing. I'm trying to know what I'm doing, and in the pursuit of that knowing, I [imitate](https://www.merriam-webster.com/dictionary/plagiarize) my betters. This process is "cargo cult" programming because I don't possess enough understanding to distinguish between the parts that I need and the parts that I am just copying because they are in his project.

All of my past projects have bloated into these nightmarish monoliths because I don't have enough experience applying sound architecture principles... I'm a competent *carpenter,* but I'm no *architect.* I can build a functional wall or a set of stairs, but I can't build an actual *building.* Or, the buildings I do build end up feeling like a random assembly of walls and stairs with no coherent unifying structure. I want to build functional buildings that don't collapse the moment someone wants me to repaint one of the walls.

I'm aware that I am probably trying to apply too many architecture ideas in this project. As it stands, there are half-baked attempts at Onion/Layered architecture, DDD aggregates, and CQRS using Mediatr scattered throughout this project. I understand the *principles* at the core of these concepts, but I've got quite a ways to go to grasp the application of these principles in practice. That's the whole point of this mess.

Since I'm thinking about it, I'll summarize the highlights so far:

### Onion/Layer/Clean Architecture

Its probably a bad sign that I am treating these like interchangable terms, but at this point in my understanding, they *are* interchangable. All of my previous applications are bloated monoliths with exactly zero attempt at layering, which means everything depends on everything else, which means if I poke any part of the application there is a butterfly effect of cascading disasters. It's brittle, and it makes it *impossible* to quickly and confidently make changes, add features, or squash bugs. 

My current understanding of these principles is that they share the same general ideas:

1. Isolation of the Domain/Application logic from implementation.
2. Inversion of dependencies... the inner layers know nothing about the outer layers, and the outer layers only know about their immediate inner layer (I know this is a simplification)

As I currently understand, the main point here is to control the cascading effect of changes. I should have to change *every* layer to accomodate a change to *any* layer. This is what I have to do for all of my past projects.

These ideas are a significant (and welcome) shift from how I was doing things before. With my previous projects, the "domain" lives mostly in the *controllers*... which is great to "get things running," but is hellish when I add anything. One of my projects has like 3 different places where the entity is added to the database, which is *insane,* considering any time I need to change a "domain" behavior, I have to make sure that the rules are applied in all three places (and probably a fourth and fifth place that I forgot about).

### Domain-Driven Design



