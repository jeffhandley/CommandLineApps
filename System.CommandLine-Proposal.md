# Proposal 1

## Layering Changes

### Invocation / Actions

All invocation models must be decoupled from the CLI model and parse result. Instead, the parse result must have the information needed to easily build an invocation model around the CLI model and parse result. As it is, every application model must find a way to adapt the desired development experience into `CliAction` APIs so that the built-in invocation model can function. This is a common challenge with "declarative" programming models, where extensibility can be challenging since all "imperative" coding approaches need to be adapted into the declarative model, which can be very limiting and awkward.

All programming models needing to adapt into the declarative model also incurs risk of precluding performance optimizations and even introducing security vulnerabilities since narrow scope scenarios cannot be tightened up to block dangerous scenarios. A hypothetical example would be a scenario where passing a malicious argument value could lead to execution of dangerous code _within_ the core of the parser, with this code running in a cloud application in a manner where the argument values are untrusted. If such an incident were to occur, the application code would likely not be in a position to mitigate the risk, and any fixes in the parser core would be breaking changes.

One of the goals with System.CommandLine must be for the parser itself to be exceptionally stable--"done" even. Invocation models must be empowered to evolve and even be supplanted though. We currently have several invocation models in place and in mind including: strongly-typed `main` programs that use source-generators, other source-generator approaches that extend top-level statements cleanly, and even interceptor-based ideas. We also have sync- and async-based invocation which has recently evolved, and there are unsolved problems involving exit codes. It's clear that invocation models must be free to evolve and multiply since each approach as trade-offs and each implementation captures a point-in-time representation of coding practices. As invocation models evolve though, we must not need to update or extend the core parser, and those invocation models must not be forced to map their programming models to the invocation model that was baked in.

The core parser must be completely devoid of any invocation model. Instead, the parse result must make building an invocation model exceptionally easy. A `CliAction`-based invocation model will still be included and promoted, but our servicing story will significantly improve with that factoring.

### Validation

There are a few validation functions baked in using a design that needs reconsidering.

1. There are validation methods exposed directly on `CliArgument<T>`
    - Those validators aren't always applicable
    - A subclass would be more appropriate for the File/Directory validators
2. There is a mixture of APIs directly on the argument, extension methods, and extensibility
    - This yields multiple validation mechanisms, which is a concept count and maintenance concern

We also need to ensure the `ArgumentResult`, `OptionResult`, and `CommandResult` inputs are sufficient and most appropriate. There's risk in coupling the parse result APIs into the CLI model APIs, as this is essentially a circular dependency. An ideal API shape would allow the CLI model APIs to be in a separate assembly from the parser itself, but striving for that ideal might introduce more API complexity than is justified.

### Completions

We should consider refactoring completions out into its own self-contained set of APIs, rather than hanging completion functionality on each of the symbol APIs. Such an approach could result in less API surface area and potentially code that's more general-purpose and reusable both in the parser and in the application code. Completions could be "registered" by symbol.

### Custom Parsers

We need to be very certain about `ArgumentResult` being the right input. Do custom parsers need to take the raw tokens, the separated tokens, the pre-parsed tokens, or any of those depending on the scenario?

Another way to approach custom parsers is to promote derived `CliOption` classes. This also removes the `CustomParser` member from the common scenarios and might even simplify the parser implementation, while also avoiding the need to define the signature for a custom parser.

Another approach would be register custom parsers, where this could be keyed by either `CliSymbol` or `Type`.

### Option Groups

Mutually-exclusive options and grouped options are relatively common scenarios. These can come in the form of "all-or-none", "exactly one", or "zero or one" configurations. What would be the prescribed approach for implementing this behavior? Would there be a relationship with arguments? Could this be represented in the help usage output? Or would this just be implemented as command validation?

## API-Level Topics

### `CliSymbol`

#### Hidden

Hidden from what? We should consider removing this from `CliSymbol` and instead configuring into the consumers what is hidden.

### `CliArgument`

#### Argument Name

It's initially confusing that arguments have names, since the name is not supplied on the command-line. The name is required for printing help and the diagram directive. The name is even used in the `ToString()` methods for convenience. It's possible this could be worked around though by generating default names such as `arg0` and `arg1` that would be printed in help and the diagram directive.

Getting argument value(s) by name is the other scenario, and it's typical for the lookup name to match the name that should show up in help.

While concentrating on a parse result shape that carries all necessary information for consumption in any invocation model, a different lookup approach may become apparent. Perhaps even one where every `CliSymbol` as a `string Key` that works like a unique key (with the parser throwing if the unique constraint is violated).

#### Default Values

The act of supplying a constant default value should be extremely easy, while the scenario of needing to produce a default value based on context should be possible. The `DefaultValueFactory` approach tries to solve both scenarios but comes at the cost of making the simple scenario harder than it should be.

As was noted with validation, we need to be certain it's best to couple parse result APIs into the CLI model APIs.

We should also attempt to remove `GetDefaultValue` from the non-generic `CliArgument` as this might lead to undesired boxing.

### `CliCommand`

Commands should have descriptions.

#### Children / Enumerable

It's not intuitive having `Children` and `GetEnumerator` APIs on a command. Supporting the collection initialization syntax is not worth the cost of these APIs being added. This is especially problematic with default usings where `System.Linq` adds a long list of extension methods into IntelliSence, making the actual members undiscoverable.

The collection initialization syntax also leads developers toward not having the symbol instances in scope, and it's a frustrating refactoring experience to need to switch from the initialization syntax to the imperative `Add` calls where the symbols are then in scope.

The `Add` methods could return the item that was added, which would allow the added symbol to easily be captured into scope without having to first declare it and then add it.

#### CliCommand.Parse

Why is the `Parse` method available on `CliCommand` instead of being restricted to `CliRootCommand`? Is there value in it being publicly exposed for sub-commands?

### `CliConfiguration`

Why is this a separate class instead of adding the configuration option members directly onto the Cli (root command)?

Some of the members on this class are related to invocation, and some are related to parsing behavior. Those need to be separated. Parsing behavior members could be moved into the Cli (root command) while invocation members would be extracted out into the invocation model. The `Error` and `Output` members should not be needed in the parser, but only in the invocation model. That separation also ensures that different invocation models could use different approaches for output.

The `ResponseFileTokenReplacer` member is an advanced scenario, and this is specific to the parser's behavior, not the CLI model itself. This member should therefore move to the parser, where it can acceptably be less discoverable.

### `CliDirective`

Directives should support aliases, as this allows CLIs to rename/obsolete directive names.

#### Directive Names

It's not entirely clear if directives' names should include the surrounding square brackets. Intuitively, the brackets would be omitted from the name, but then seeing that options require the "--" and "-" prefixes in the names and aliases, that adds uncertainty. See the notes about names in `CliOption` and `CliArgument` as well.

### `CliOption`

#### Invocation

Invocation of options is an interesting API challenge. While we need to exclude any specific invocation model from the option API, and it's atypical for an option to have its own invocation, there's value in all invocation models being able to share the same invocation precedence logic that is currently baked into the parser. To produce information on the parse result that makes it clear what symbol should be invoked, it's possible that an option needs to carry information that it can be invoked, thus taking precedence over the command it's a child of.

Alternatively, instead of addressing this problem of invocation precedence within the symbol APIs though, documentation could cover the scenario of options that have their own invocation. That documentation could include guidelines for how precedence should be implemented. While we want to embrace the idea that invocation models will come and go and otherwise evolve, there will be a very small number of invocation models compared to the number of CLI applications.

An invocation model that uses a "registration" programming model, where commands are registered or "mapped", that registration system could support various `CliSymbol` types being registered. For example, all directives, commands, and sub-commands would need to be registered, and options could also be registered. Precedence logic would likely be: Directives, Options (in order of registration), Commands (and their sub-commands).

#### Default Values

The API for default option values needs to more clearly indicate whether the default value applies when the option-argument is not supplied vs. when the option is not specified at all.

We should also attempt to remove `GetDefaultValue` from the non-generic `CliOption` as this might lead to undesired boxing.

#### Recursive options

Another name for "Recursive" would be "AppliesToSub-Commands".

This approach of an option declaring that it applies to sub-commands has a potential flaw though: sub-commands are not in control of which parent options apply, and there is no obvious way for a sub-command to override which options from ancestors _do not_ apply. An alternate or complementary approach would be to allow sub-commands to control which ancestor options apply.

Instead of options having APIs related to the command/sub-command concept, it could be worth considering moving this information to the CliCommand API to contain the concept there.

#### Option Names

Ideally, developers would not need to specify the "--" and "-" prefixes on the option names. In the most common scenarios, simply supplying a name could yield the full name being registered with the "--" prefix and the first character being registered as an alias with the "-" prefix. This would of course feed into POSIX bundling as well. That default behavior could be overridden through additional parameters/properties.

By allowing option names to implicitly be prefixed with "--" and "-", this would address the uncertainty with directive names and the surrounding brackets. That omission would also improve name-based lookup of options as the "--" would not need to be included there either. Name collisions between options, arguments, and directives do become more likely with this approach, but that can be addressed with the unique `Key` mentioned above.

### Option and Argument Arity

Currently, arity can be inferred between single values and enumerables of values based on the type of `T`. Specifying the `T` for enumerable values is awkward though, as it's not obvious what enumerable types are supported or optimized.

If the APIs between singular values and enumerables were separated, instead of just being handled by different `T` types, this could unlock better type specialization optimizations while also making the enumerable scenarios clearer. While it's easy to separate the methods for getting the values from a result, this distinction during the time of creating/adding options/arguments is less straightforward.

Separating the single and enumerable scenarios might also make the relationship between nullable value-types, nullable reference-types, required options/arguments, and arity more intuitive overall.

### `CliSymbol`

#### Name

On the surface, it's unclear if a symbol's name should include the extra characters associated with it. Does a directive name include the surrounding brackets? Does an option name include the "--" prefix? Does a sub-command's name include the ancestors' names? What about when a sub-command has multiple peer parents?

Perhaps abstract symbols shouldn't inherently have a name at all, and only the derived symbols would.

It's also noteworthy that options inherently use arguments as an implementation detail. What should the name or key for an option's argument be? Since this is an implementation detail, the API surface is not mandated to accommodate the scenario.

### `ParseResult`

#### CommandResult and RootCommandResult

Why do both of these exist and which is intended to be consumed?

#### Argument and Option Values

The methods for getting values for arguments and options should not return `T?`, but instead return `T`. Those methods can throw if the argument or option isn't specified and there wasn't an unspecified value configured. This throwing, non-null return approach eliminates the need for handling possibly-null return values for arguments and options that were configured to be required and/or with unspecified values configured. Calling the method that returns an enumerable could succeed for single-value arguments/options, but calling the method that returns a single value would throw for enumerable arguments/options.
