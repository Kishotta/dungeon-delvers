import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { CharacterService } from 'src/app/data/services/api/character.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  constructor(
    private authService: OAuthService,
    private characterService: CharacterService
  ) {}

  login() {
    this.authService.initImplicitFlow();
  }

  logout() {
    this.authService.logOut();
  }

  get givenName() {
    const claims = this.authService.getIdentityClaims();
    if (!claims) {
      return null;
    }
    return claims['given_name'];
  }

  ngOnInit() {
    this.characterService.getCharacters().subscribe((characters) => {
      console.log(characters);
    });
  }
}
