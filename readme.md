# AutoDealer Registry

A simple .NET 7 console application for managing an auto-dealer’s inventory.\
Add, list, sort, filter, remove, bulk-load vehicles via a CSV-style text file, and apply decorator-based upgrades to vehicles.

---

## Table of Contents

1. [Features](#features)
2. [Getting Started](#getting-started)
3. [Usage](#usage)
4. [Project Structure](#project-structure)
5. [Decorator Upgrades](#decorator-upgrades)
6. [Testing](#testing)
7. [Tools & Technologies](#tools--technologies)
8. [Contributing](#contributing)
9. [License](#license)
10. [Contact](#contact)

---

## Features

- **Add vehicles** (Car, MiniBus, Motorbike) with make, model, year, price, color, horsepower, fuel type
- **List** current inventory
- **Sort** by any property (Brand, Year, Price, etc.) in ascending/descending order
- **Filter** by property using operators (Equals, GreaterThan, Contains, etc.)
- **Remove** vehicles by ID
- **Bulk-load** vehicles from a CSV-compatible text file with headers
- **Decorator Upgrades**: modify vehicles by attaching decorators (e.g., BetterEngine, ElectricSeats) to add optional features

---

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

### Installation

```bash
# Clone the repository
git clone https://github.com/SNNikolob99/AutoDealer.git
cd AutoDealer

# Build all projects
dotnet build
```

---

## Usage

Run the console app:

```bash
cd ConsoleApp
dotnet run
```

You’ll see a menu:

```
======== AutoDealer Registry =========
1. Add Vehicle
2. List Vehicles
3. Sort Vehicles
4. Filter Vehicles
5. Remove Vehicle
6. Get total sum
7. Modify vehicle by index
8. Exit
Select an option:
```

- **Add**: follow prompts for type, brand, model, etc.
- **List**: view all vehicles, then press any key to return.
- **Sort/Filter**: specify property and sort/filter values.
- **Load**: point at a CSV text (with header row):
  ```csv
  Type,Brand,Model,Year,Price,Color,HorsePower,FuelType
  Car,Ford,Focus,2019,20000,Red,150,Gasoline
  ...
  ```
- **Remove**: enter the vehicle’s ID.
- Modify: Enter the vehicle\`s ID. Then you will be represented with options to add

---

## Project Structure

```
/AutoDealer
  /Decorators  ← decorator classes(V6TDIEngine,LongerBase,etc.)
  /Models      ← core classes (Vehicle, Car, MiniBus, Motorbike, AutoRegistry)
  /ConsoleApp  ← Program.cs (menu UI, calls into Models)
  /Tests       ← xUnit test project with FluentAssertions
README.md
LICENSE
```

---

## Decorator Upgrades

This project uses the **Decorator Pattern** to attach optional features to vehicles at runtime:

- Decorator classes derive from `VehicleDecorator` (and typed `CarDecorator`, `MinibusDecorator`, etc.).

- Examples: V6 TDI Engine, Hybrid Traction , Designer color.

- Each decorator implements `AttachPart()` to modify the wrapped vehicle’s properties or state.

---

## Testing

From the solution root, run:

```bash
dotnet test
```

Tests are written with **xUnit** and **FluentAssertions** to cover:

- Adding/removing vehicles
- Sorting and filtering logic
- Bulk-loading from file
- Validation and error cases

---

## Tools & Technologies

- **.NET 7** (C# 11)
- **xUnit** for unit testing
- **FluentAssertions** for expressive assertions
- **Decorator Pattern** for vehicle upgrades
- **Reflection** for dynamic sort/filter

---

## Contributing

1. Fork the repository
2. Create a branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (follow existing code style)
4. Add/modify tests to cover new behavior
5. Submit a pull request

Please keep changes focused and well-tested.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Contact

- GitHub: [https://github.com/SNNikolov99](https://github.com/SNNikolov99)
- Issues: [https://github.com/SNNikolov99/AutoDealer/issues](https://github.com/SNNikolov99/AutoDealer/issues)
- Email: [SNNikolov99@gmail.com](mailto\:SNNikolov99@gmail.com)

