import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, provideHttpClient } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { OAuthModule, provideOAuthClient } from 'angular-oauth2-oidc';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './core/dashboard/dashboard.component';
import { CoreModule } from './core/core.module';
import { CharacterListComponent } from './features/character-list/character-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DashboardComponent,
    CharacterListComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CoreModule,
    OAuthModule.forRoot(),
  ],
  providers: [provideHttpClient(), provideOAuthClient()],
  bootstrap: [AppComponent],
})
export class AppModule {}
