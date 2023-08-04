
## Behavioral notes for the APIs that don't model the CLI upfront

### Names and single-character aliases
- The '--' prefix is inferred for option names
- The first character of the option name is inferred as the single-character alias
- The '-' prefix is inferred for single-character aliases; bundling works by default
- If there are collisions on option names or abbreviations, an exception is thrown
- We can determine if overriding the prefixes should be available (if so, why?)

### Required and nullable values
- If a non-nullable option is not specified, an exception is thrown
- If a nullable option is not specified, the default value is null

## How it would work:
- Under the covers, the parse result goes through iterative parsing
- At first, the result would have lots of "unrecognized tokens"
- After each call to `GetOption()` or `GetArguments()`, tokens would become recognized
- The unrecognized tokens list then shrinks, with the recognized tokens growing
- The consumer could check `UnrecognizedTokens` or `HasUnrecognizedTokens` if desired
- If the sequence of calls to get the options or arguments is detected as potentially
  producing ambiguous results, an exception would be thrown.

## What about more advanced features like sub-commands?
- Optimize for a progressive disclosure approach
- Guide users toward modeling the CLI in order to use intermediate and advanced features
