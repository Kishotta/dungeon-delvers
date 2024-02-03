import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'dungeon-delvers-spa';
  constructor(private oauthService: OAuthService) {
    this.oauthService.redirectUri = window.location.origin;
    this.oauthService.clientId = '2598a7ba-27c1-41d2-93a3-e3d941d8967f';
    this.oauthService.scope = 'openid profile email offline_access';
    this.oauthService.oidc = true;
    this.oauthService.skipIssuerCheck = true;
    this.oauthService.setStorage(sessionStorage);
    let url = 'http://localhost:9011/.well-known/openid-configuration';
    this.oauthService.loadDiscoveryDocument(url).then(() => {
      this.oauthService.tryLogin();
    });
  }
}
