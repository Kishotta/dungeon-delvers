import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  constructor(private authService: AuthService) {}

  public login() {
    this.authService.login();
  }

  public logout() {
    this.authService.logout();
  }

  public get userName() {
    if (this.authService.isAuthenticated === false) return null;

    var claims = this.authService.claims;
    if (!claims) return null;

    return claims['email'];
  }
}
