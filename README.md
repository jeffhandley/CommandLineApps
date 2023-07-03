# Command-Line Apps
Sample command-line apps for illustrating different stages of command-line app scenarios.

## 1. Console App (Fully Decomposed, Procedural Code)
I'm just using the parser to get strongly-typed args. There's no Help, Completions, Error Reporting, Actions, or Invocation. There are no new concepts such as strongly-typed main methods, just top-level statements and procedural code that allows bare-minimum distraction from the task at hand. The code gets the parse result and then either shows hand-rolled error messages (Console.WriteLine) or continues into the logic of the app.

This targets the first-time .NET user as well as everyone who is just building a Console App and they aren't ready to dive directly into a well-architected, user-friendly CLI App.

## 2. Console App with End-User Delight (Composition of Helpers into Procedural Code)
My top-level statements Console App still directly calls the parser to get the parse results. But I've now added calls into helper features for printing help, showing errors, and even serving completions; I'm amazed at how easy each of those was to add in! When none of these end-user delight scenarios are hit, I then still just invoke my own code without any function calls. My Console App still only does one thing, so there are no commands, no "actions" or "invocation".

This targets the user whose Console App has begun to take shape, it's no longer just a throwaway project/prototype, and they want to start to bake user-friendliness in early. The logic of the app itself is still very simple though.

## 3. Console App with Commands (Composition of Helpers and Commands into Procedural Code)
My app has now grown up and I have a couple commands. I declare actions associated with those commands, and my top-level statements have code to use those same helper features for help, completions, and error reporting, but then if none of those applies, I can also simply allow the targeted command to be invoked. I've considered switching back to having a main because I want to define methods for my commands' actions.

This targets the user who has begun to see that their one-off Console App has the potential to become a CLI App.

## 4. CLI App (Fully Recomposed, Declarative App Model)
My app has now grown up, I have a couple commands and I want help, completions, and error reporting taken care of for me automatically while using lovely APIs that also let me declare actions so that invocation is taken care of too. I've switched to a main now, with additional methods that capture the actions of each of my commands. My main is really trim though, with no procedural code required for help, completions, error reporting, or invoking the selected command. It feels very declarative and it works nicely.

This targets today's System.CommandLine user as well as anyone else who has found themselves building a CLI App that they need to maintain over time and that maybe even ships to a broad audience.
