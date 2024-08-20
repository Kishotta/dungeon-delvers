import {Component, ElementRef} from '@angular/core';
import {NgClass} from "@angular/common";

@Component({
  selector: 'card-body',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './card-body.component.html',
  styleUrl: './card-body.component.css'
})
export class CardBodyComponent {
  constructor(private elementRef: ElementRef) {}

  get cardBodyClasses() {
    return {
      [this.elementRef.nativeElement.getAttribute('class') ?? '']: true,
      'px-4 py-5 sm:p-6': true
    };
  }
}
