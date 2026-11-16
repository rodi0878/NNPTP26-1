# Newton Fractal Generator (NNPTP Zápočet 1)

Console application for generating Newton fractals for polynomial $f(z) = z^3 + 1$.

## Usage

Build solution in Visual Studio or with MSBuild:

```powershell
NNPTPZ1.exe <width> <height> <xmin> <xmax> <ymin> <ymax> <outputpath>
```

Example:

```powershell
NNPTPZ1.exe 800 600 -2 2 -1.5 1.5 out.png
```

## Refactoring Overview

- Separated monolithic `Program.cs` into individual classes:
  - `Mathematics/ComplexNumber.cs` - complex number representation and operations.
  - `Mathematics/Polynomial.cs` - polynomial operations, Horner method evaluation, derivative calculation.
  - `FractalParameters.cs` - CLI arguments parsing and boundary validation.
  - `NewtonFractalRenderer.cs` - fractal pixel rendering, Newton iteration, root management, pixel coloring.
  - `Program.cs` - entry point with basic error handling.
- Fixed inverted pixel coordinates (X/Y mapping) for rectangular images.
- Replaced naive polynomial evaluation with Horner's method.
- Replaced float precision with double in complex arithmetic.
- Fixed off-by-one root index coloring.
- Added convergence checks to prevent infinite loops in Newton iteration.
- Wrapped `Bitmap` in `using` block to prevent GDI+ resource leaks.
- Added unit tests for complex math, polynomials, arguments parsing and fractal rendering.
