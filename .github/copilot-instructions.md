# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

This RimWorld mod is designed to expand the player's ability to interact with in-game environments and enhance the joy activities available to colonists. It introduces new activities, thoughts, and interactions tailored towards specific traits, providing a deeper and more immersive gameplay experience. The mod serves as an add-on for players looking to diversify and enrich their RimWorld stories.

## Key Features and Systems

- **Joy Activities**: Introduces a range of new joy activities such as eating dirt, staring at the ceiling, and enjoying various weather conditions. These activities enrich the way colonists spend their leisure time.
  
- **Trait-Based Activities**: Unique activities that are unlocked based on a colonist's traits, providing personalized interactions with the world.

- **Expanded Thoughts**: Adds new thought processes tied to activities and environments to simulate more realistic colonist behaviors.

- **World Components**: Includes a world component that monitors trait-based interactions, enhancing the dynamic nature of gameplay.

## Coding Patterns and Conventions

- **Naming Conventions**: Classes and methods use PascalCase for easy readability, while private fields use camelCase.

- **Access Modifiers**: The project uses a mix of `public` and `internal` modifiers strategically to restrict access based on necessity, enhancing security and encapsulation.

- **Class Design**: Utilizes both internal and public classes, organized into systems for joy givers, job drivers, and thought workers to maintain modularity.

- **Extensions**: Static classes are used for helper methods, promoting reusable and organized code.

## XML Integration

- The project is set up to integrate XML-based definitions extensively, allowing for easy mod updates and adjustments. Utilize XML for defining:

  - Activities and jobs
  - Traits and their corresponding behaviors
  - Thought processes associated with environments or actions

## Harmony Patching

- **Harmony**: The mod employs Harmony for non-intrusive modifications of RimWorld's base code. Harmony allows the mod to add new joy activities and thoughts without overriding the core game files, ensuring compatibility with other mods.

- **Patch Initialization**: All patches are initialized via the `HarmonyInit` class. Ensure all patches are appropriately annotated within this class to maintain organized and traceable modifications.

## Suggestions for Copilot

- When adding new joy activities or thoughts, use existing classes as templates. Copilot can assist by auto-generating the necessary methods and references for integrating these new features.

- For adding methods to static utility classes, use Copilot to suggest efficient code extensions and error handling routines.

- When working with XML integration, leverage Copilot for boilerplate generation, such as XML definition templates, to ensure seamless mod content additions.

- Use Copilot suggestions for implementing Harmony patches by analyzing similar, existing patches to minimize compatibility issues and ensure consistent mod behavior.

This instruction set serves as a guide for developers using GitHub Copilot to extend and maintain the mod efficiently. It provides a structured approach to adding new content while ensuring consistency and compatibility.
