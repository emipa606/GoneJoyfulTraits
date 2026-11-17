# GitHub Copilot Instructions for Gone Joyful Traits (Continued)

## Mod Overview and Purpose
Gone Joyful Traits (Continued) is an update to the "gwen blanket specters" mod. It adds 16 new traits that affect how colonists in the RimWorld game experience joy and interact with their environment. These traits are designed to enhance the gameplay by providing additional depth to character interactions and behaviors.

## Key Features and Systems
- **16 New Traits:** Each trait provides unique bonuses or penalties related to how colonists experience joy, interact with the environment, and perform various activities.
- **Integration with Existing Game Mechanics:** The traits interact with existing joy givers, jobs, and thought systems to create a cohesive and immersive experience.
- **XML Definitions:** All new traits and related game elements are defined in XML files, ensuring they are easily integrated into the game's data system.
- **Harmony Patching:** C# code uses Harmony patches to modify the game's behavior and ensure that the new traits work seamlessly with the existing codebase.

## Coding Patterns and Conventions
- **Modular Code Structure:** The C# code is organized into modules, with each module responsible for a specific aspect of the mod (e.g., handling joy givers, jobs, thoughts).
- **Consistent Naming Conventions:** All classes, methods, and variables use clear and consistent naming conventions to improve code readability and maintainability.
- **Comprehensive Documentation:** Comments and documentation are provided throughout the code to explain complex logic and facilitate future development.

## XML Integration
- **XML Files:** The mod defines new traits, joy givers, jobs, and thoughts in XML files located in the `Defs` directory. Each XML file corresponds to a specific type of game element.
- **Def Definitions:** Each XML file contains `Def` elements that describe the properties of the new traits, joy givers, jobs, and thoughts.
- **Data Loading:** The game's data system automatically loads these XML files at startup, making them available for use in the game.

## Harmony Patching
- **Harmony Library:** The mod uses the Harmony library to patch existing game methods, allowing for modifications to the game's behavior without altering the original code.
- **Patch Methods:** Postfix and prefix methods are used to modify the behavior of existing methods, ensuring that the new traits function correctly.
- **Patch Management:** The `HarmonyInit.cs` file initializes the Harmony patches and ensures they are applied at the appropriate time.

## Suggestions for Copilot
- **Code Suggestions:** When using Copilot to generate code, focus on suggestions that align with the established coding patterns and conventions. This will help maintain consistency and readability in the codebase.
- **XML Suggestions:** For XML file generation, request suggestions that include all necessary `Def` elements and properties. Ensure that the XML files are well-structured and adhere to the game's data system requirements.
- **Harmony Patch Suggestions:** When generating Harmony patches, request suggestions that include both postfix and prefix methods as needed. Ensure that the patches are correctly applied and do not interfere with existing game mechanics.

By following these guidelines, developers can effectively use GitHub Copilot to enhance the Gone Joyful Traits (Continued) mod, ensuring that it remains well-structured, maintainable, and aligned with the RimWorld game's mechanics.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

