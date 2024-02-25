import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OAuthModule, OAuthService } from 'angular-oauth2-oidc';

@NgModule({
  declarations: [],
  imports: [CommonModule, OAuthModule.forRoot()],
})
export class CoreModule {
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
}
