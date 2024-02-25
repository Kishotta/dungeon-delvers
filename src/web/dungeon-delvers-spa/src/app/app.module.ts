import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MainpageComponent } from './layout/mainpage/mainpage.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';

import { SharedModule } from './shared/shared.module';
import { OAuthService } from 'angular-oauth2-oidc';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { CharacterListComponent } from './modules/characters/components/character-list/character-list.component';

@NgModule({
  declarations: [
    AppComponent,
    MainpageComponent,
    HeaderComponent,
    FooterComponent,
    CharacterListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoreModule,
    SharedModule,
  ],
  providers: [
    {
      provide: LocationStrategy,
      useClass: PathLocationStrategy,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
