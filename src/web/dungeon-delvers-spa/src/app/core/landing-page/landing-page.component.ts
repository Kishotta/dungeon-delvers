import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss'],
})
export class LandingPageComponent {
  constructor(public authService: AuthService) {}
  // store 5 images to use in the landing page
  images = [
    'assets/images/landing-page/rogue.webp',
    'assets/images/landing-page/druid.webp',
    'assets/images/landing-page/fighter.webp',
    'assets/images/landing-page/knight.webp',
    'assets/images/landing-page/wizard.webp',
  ];

  // expose the images to the html
  getImages() {
    return this.images;
  }
}
