# Dungeon Delvers: Character Management Module

## Project Overview

### Introduction

Dungeon Delvers is a digital tool designed to enhance the Dungeons & Dragons 5th Edition (D&D 5e) gaming experience for both Game Masters (GMs) and players. This application streamlines the process of character creation, management, and progression, facilitating a more immersive and organized gameplay experience. By providing an intuitive and comprehensive platform, Dungeon Delvers allows users to focus on the storytelling and strategic aspects of the game, minimizing the manual paperwork and calculations often associated with complex role-playing games.

### Target Audience

The primary users of Dungeon Delvers are:

- **Game Masters (GMs)**, who seek to efficently manage game settings, player characters, and campaign details.
- **Players**, who require a user-friendly interface to create,customize, and evolve their characters over the course of their adventures.

### Core Functionalities

The initial focus of Dungeon Delvers is the Character Management Module, which offers the following key features:

- **For GMs**:
  - Ability to set available sources (e.g., rulebooks, content) for character creation within their campaigns.
  - The capability to create and manage homebrew content with the same depth and functionality as published content. This includes races, subclasses, items, spells, and unique rules (e.g., auxiliary levels, starting feats).
  - Overview and management of all player characters in their campaign, including approval processes for character modifications.
    
- **For Players**:
  - Guided character creation process that adheres to the GM's specified sources and rules.
  - Detailed character sheets that include levels, traits, features, abilities, feats, hit points (HP), inventory, spells, and more.
  - An interactive interface to track character progression, manage inventory and spells, and update character details as the campaign evolves.

### Goal

The oal of Dungeon Delvers is to provide a seamless and flexible platform that supports the dynamic and creative nature of D&D 5e. By reducing the administrative overhead associated with character and campaign management, Dungeon Delvers aims to enrich the gaming experience for both GMs and players, making it more engaging, accessible, and enjoyable.

## Functional Requirements

General Requirements

1. **User Accounts and Authentication**
    - Users must be able to create accounts using an email address and password.
    - The application must provide secure authentication mechanisms, including password recovery and, optionally, multi-factor authentication (MFA).
2. **User Roles**
    - Two primary roles: Game Master (GM) and Player.
    - Users can switch roles (GM/Player) based on the campaign context.
3. **Accessiblity and Usability**
    - The application must be responsive and accessible on various devices, including desktops, tablets, and smartphones.
    - UI/UX should be intuitive, allowing easy navigation and access to features for users of all experience levels.

For Game Masters (GMs)

1. **Campaign Creation and Management**
    - GMs can create campaigns, setting a title, description, and specific rules or restrictions (e.g., allowed source material).
2. **Homebrew Content Creation**
    - Interface for creating and managing homebrew content with the same level of detail as oficial content, including:
      - Races, Classes, Subclasses
      - Items, Spells
      - Custom rules (auxiliary levels, starting feats, etc.)
    - Homebrew content can be made private to a campaign or shared with the Dungeon Delvers community. ⚠️
3. **Player Management**
    - Ability to invite players to join a campaign.
    - Tools for reviewing and approving player characters, including modifications as characters evolve.

For Players

1. **Character Creation**
    - Step-by-step character creation process that adheres to the rules and restrictions set by the GM, including selection from both official and homebrew content.
    - Dynamic character sheet that updates based on selections (race, class, abilities, etc.).
2. **Character Management**
    - Interface for manageing character details throughout the campaign, including:
      - Leveling up
      - Tracking HP, spells, inventory, and other stats
      - Updating traits, features, abilities, and feats
3. **Campaign Interaction**
    - Ability to join campaigns by inveitation from GMs.
    - Tools for submitting character changes to GMs for approval.

System-Wide Features

1. **Content Searching and Filtering**
   - Robust search functionality for both official and homebrew content, with filters for type, source, campaign, etc.
2. **Community Features** ⚠️
   - Options for sharing homebrew content with the broader Dungeon Delvers community. ⚠️
   - Rating and feedback system for community-contributed content. ⚠️
3. **Notifications and Updates**
   - Real-time notifications for campaign invetations, GM approvals, and other key interactions.
   - Update mechanism for the application to recieve new official content and updates.

## Non-Functional Requirements

Performance

1. **Response Time**: The application should display pages and respond to user inputs within 2 seconds under normal load conditions.
2. **Load Capacity**: Support concurrent usage by up to 1000 users without significant degradation in performance.

Security

1. **Data Protection**: Implement industry-standard encryption for data at rest and in transit. This includes using HTTPS for all communications and encrypting sensitive data stored in databases.
2. **User Authentication**: Secure authentication mechanisms, including secure password storage (e.g., hashing with a salt) and the option for multi-factor authentication.
3. **Access Control**: Enforce role-based access controls (RBAC) to ensure users can only access data and functionalities relevant to their role (GM or Player).

Usability

1. **Accessibility**: The application should comply with WCAG 2.1 AA standards to ensure it's accessbile to users with disabilities.
2. **Mobile Responsiveness**: Ensure the application is fully functional and visually coherent on a variety of devices, including desktops, tablets, and smartphones.

Scalability

1. **Infrastructure Scalability**: Design the backend and database architecture to be easily scalable to accommodate growing numbers of users and data volume.
2. **Maintainability**: Codebase and architecture shoudl be organized and documented to facilitate easy updates, bug fixes, and future additions.

Reliability

1. **Uptime**: Aim for 99.9% uptime, excluding scheduled maintenance windows.
2. **Data Integrity and Backup**: Regularly back up data and implement robust error handling and recovery processes to prevent data loss.

Legal and Compliance

1. **Copyright Compliance**: Develop and implement a strategy for managing copyrighted content, ensuring that users can only input data for personal use and are aware of the legal implications for sharing copyrighted materials.
2. **Privacy Compliance**: Adhere to privacy laws relevant to the application's user base (e.g., GDPR, CCPA) including user content for data collection and providing users with the ability to view, edit, and delete their personal information.
