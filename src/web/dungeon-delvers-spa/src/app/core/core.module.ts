import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [LandingPageComponent, NotFoundComponent],
  imports: [CommonModule],
  exports: [LandingPageComponent],
})
export class CoreModule {}
