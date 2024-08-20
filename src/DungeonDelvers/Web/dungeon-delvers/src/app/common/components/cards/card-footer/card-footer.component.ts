import {Component, ElementRef} from '@angular/core';
import {NgClass} from "@angular/common";

@Component({
  selector: 'card-footer',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './card-footer.component.html',
  styleUrl: './card-footer.component.css'
})
export class CardFooterComponent {
  constructor(private elementRef: ElementRef) {}

  get cardFooterClasses() {
    return {
      [this.elementRef.nativeElement.getAttribute('class') ?? '']: true,
      'px-4 py-4 sm:px-6': true
    };
  }
}
