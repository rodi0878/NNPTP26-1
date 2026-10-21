# Newton fractal generator

A C#/.NET Framework 4.8 console application that renders the Newton fractal
for the polynomial **f(z) = z³ + 1**. The generated bitmap colors pixels by
which root the iteration converges to, with darker colors for slower convergence.

## Usage

Build `NNPTPZ1.sln` in Visual Studio with .NET Framework 4.8 and run:

```text
NNPTPZ1.exe 800 600 -2 2 -1.5 1.5 fractal.png
```

Arguments (in this order): image width, image height, minimum X, maximum X,
minimum Y, maximum Y, output file. Use `.` as the decimal separator. Both image
dimensions must be positive; minimum bounds must be smaller than maximum bounds.
The output file's directory must already exist.

## Structure

- `FractalOptions`: validates and holds command-line options.
- `Mathematics/ComplexNumber`: complex arithmetic with immutable values.
- `Mathematics/Polynomial`: evaluation (Horner's method) and differentiation.
- `NewtonFractalRenderer`: Newton iteration, root identification and coloring.
- `Program`: command-line entry point and file output.
- `NNPTPZ1Tests`: MSTest regression tests for arithmetic, configuration and image dimensions.

## Refactoring notes

The original solution placed mathematical types, image generation and argument
parsing in a single long `Program.cs`. The code is now split into classes with
one clear responsibility each. It uses descriptive names, small methods, named
constants, no dead/duplicated code, and deterministic disposal of bitmaps.

Functional corrections intentionally included in this patch:

1. A rectangular bitmap now uses `(x, y)` coordinates correctly (previously
   width/height loop indices were reversed).
2. Newton iteration has a maximum number of steps and checks for divergence
   or zero derivatives instead of potentially looping forever. Nonconverging
   pixels are black. This may change pixel colors compared with the old image.
3. Known roots are indexed consistently (the original mixed zero-based and
   one-based indices, assigning inconsistent colors).
4. Imaginary components now have `double` precision rather than `float`.
5. Arguments are validated, and file output errors are reported to stderr.

These are behavioral fixes, not strictly behavior-preserving refactorings.

To build and run the tests from a clean clone, restore the original
`packages.config` dependencies using `nuget.exe` (available from NuGet.org):

```powershell
nuget restore .\NNPTPZ1.sln -PackagesDirectory .\packages -Source https://api.nuget.org/v3/index.json
msbuild .\NNPTPZ1.sln /p:Configuration=Debug
vstest.console.exe .\NNPTPZ1Tests\bin\Debug\NNPTPZ1Tests.dll /Logger:console
```

The generated `bin`, `obj` and `packages` directories are ignored by Git.
