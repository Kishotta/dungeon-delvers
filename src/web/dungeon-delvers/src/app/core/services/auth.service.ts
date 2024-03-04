import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private oauthService: OAuthService) {
    this.oauthService.configure({
      redirectUri: window.location.origin,
      clientId: 'ddelvers',
      scope: 'openid profile email',
      issuer: 'http://localhost:5000/realms/ddelvers',
      responseType: 'code',
      showDebugInformation: true,
    });
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  public login() {
    this.oauthService.initImplicitFlow();
  }

  public logout() {
    this.oauthService.logOut();
  }

  public get isLoggedIn() {
    return this.oauthService.hasValidAccessToken();
  }

  public isAuthenticated() {
    return this.oauthService.hasValidAccessToken();
  }

  public get email() {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) {
      return null;
    }
    return claims['email'];
  }

  public getAuthHeaders() {
    return {
      Authorization: 'Bearer ' + this.oauthService.getAccessToken(),
    };
  }
}
