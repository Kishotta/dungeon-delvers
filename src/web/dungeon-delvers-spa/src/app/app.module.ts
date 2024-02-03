import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, provideHttpClient } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {
  AuthConfig,
  OAuthModule,
  provideOAuthClient,
} from 'angular-oauth2-oidc';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
  ],
  providers: [provideHttpClient(), provideOAuthClient()],
  bootstrap: [AppComponent],
})
export class AppModule {}
