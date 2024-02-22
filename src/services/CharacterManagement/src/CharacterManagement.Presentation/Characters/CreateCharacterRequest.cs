using System.ComponentModel.DataAnnotations;

namespace CharacterManagement.Presentation.Characters;

/// <summary>
/// Create Character Request
/// </summary>
/// <param name="Name">Character Name</param>
public record CreateCharacterRequest([property: Required]string Name);
