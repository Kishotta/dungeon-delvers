import {Component, ElementRef} from '@angular/core';
import {NgClass} from "@angular/common";

@Component({
  selector: 'card-header',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './card-header.component.html',
  styleUrl: './card-header.component.css'
})
export class CardHeaderComponent {
  constructor(private elementRef: ElementRef) {}

  get cardHeaderClasses() {
    return {
      [this.elementRef.nativeElement.getAttribute('class') ?? '']: true,
      'px-4 py-5 sm:px-6': true
    };
  }
}
