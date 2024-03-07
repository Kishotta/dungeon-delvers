import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Observable } from 'rxjs';
import { Race } from './race.interface';

@Injectable({
  providedIn: 'root',
})
export class RaceService {
  private baseUrl = 'http://localhost:8080/races';

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {}

  public getRaces(): Observable<Race[]> {
    return this.httpClient.get<Race[]>(this.baseUrl, {
      headers: this.authService.getAuthHeaders(),
    });
  }

  public getRaceById(raceId: string) {
    return this.httpClient.get<Race>(`${this.baseUrl}/${raceId}`, {
      headers: this.authService.getAuthHeaders(),
    });
  }

  public createRace(name: string) {
    return this.httpClient.post(
      this.baseUrl,
      { name },
      { headers: this.authService.getAuthHeaders() }
    );
  }

  public addTrait(race: Race | undefined, name: string, description: string) {
    if (!race) return;

    return this.httpClient.post(
      `${this.baseUrl}/${race.id}/traits`,
      {
        name,
        description,
      },
      {
        headers: this.authService.getAuthHeaders(),
      }
    );
  }
}
