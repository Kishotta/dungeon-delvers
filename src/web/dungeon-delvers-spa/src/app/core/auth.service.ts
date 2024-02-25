import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({ providedIn: 'root' })
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

  login() {
    this.oauthService.initImplicitFlow();
  }

  logout() {
    this.oauthService.logOut();
  }

  get token() {
    return this.oauthService.getAccessToken();
  }

  get identityClaims() {
    return this.oauthService.getIdentityClaims();
  }

  get hasValidToken() {
    return this.oauthService.hasValidAccessToken();
  }
}
