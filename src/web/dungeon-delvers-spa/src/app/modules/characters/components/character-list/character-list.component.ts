import { Component } from '@angular/core';
import { Character } from 'src/app/data/interfaces/character.interface';
import { CharacterService } from 'src/app/data/services/api/character.service';

@Component({
  selector: 'app-character-list',
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.scss'],
})
export class CharacterListComponent {
  characters: Character[] = [];

  constructor(private characterService: CharacterService) {}

  ngOnInit() {
    this.getCharacters();
  }

  getCharacters() {
    this.characterService.getCharacters().subscribe((characters) => {
      this.characters = characters;
    });
  }
}
