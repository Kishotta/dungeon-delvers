import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Character } from '../../interfaces/character.interface';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root',
})
export class CharacterService {
  private baseUrl = 'http://localhost:8080/characters';

  constructor(private http: HttpClient, private oauthSerivce: OAuthService) {}

  getCharacters() {
    var headers = {
      Authorization: 'Bearer ' + this.oauthSerivce.getAccessToken(),
    };
    return this.http.get<Character[]>(this.baseUrl, { headers });
  }
}
