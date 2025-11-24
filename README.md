# Markov-Text-Generation
Text generation using [Markov Chaining](https://en.wikipedia.org/wiki/Markov_chain).

## Installation
Run the following command to download the project.
```bash
git clone https://github.com/gmcgehee/Markov-Text-Generation.git
```

## Usage
This is a dotnet project. [Install the dotnet development platform](https://dotnet.microsoft.com/en-us/download) in order to use this project.

In order to run the projct, first navigate to src:
```bash
cd src/
```

The following command will be used to build and run the project:
```bash
dotnet run <text filepaths> <n-length> <story wordcount>
```

Think of `<text filepaths` like a string of filepaths, delineated by spaces, specifying files to be used to generate Markov Chains. `n-length` is the word count for each token to be used in the Markov Chains. `story wordcount` is the length of the story. The higher the `n-length`, the more natural the text generation will sound. Unfortunately, it will also be less original. Sample texts have been provided in `texts/`.

A sample run might look like this:
```bash
dotnet run ../texts/drseuss.txt 2 150
```

Note: The program will output three different Markov models created with three different data structures. Each story will be different. Expect a very large output for a high `story wordcount`.

## Bugs
There are several bugs to be fixed in any future updates. The generated texts sometime crash.
