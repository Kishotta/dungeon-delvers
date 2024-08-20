import {AfterContentInit, Component, ContentChildren, ElementRef, QueryList, ViewChild} from '@angular/core';
import {NgClass, NgTemplateOutlet} from "@angular/common";

@Component({
  selector: 'card-heading',
  standalone: true,
  imports: [
    NgClass,
    NgTemplateOutlet
  ],
  templateUrl: './card-heading.component.html',
  styleUrl: './card-heading.component.css'
})
export class CardHeadingComponent implements AfterContentInit {
  @ViewChild('actionsContainer', { static: true })
  actionsContainer?: ElementRef;

  @ContentChildren('action')
  actionElements?: QueryList<ElementRef>;

  constructor(private elementRef: ElementRef) {}

  ngAfterContentInit() {
    this.actionElements?.forEach(action => {
      this.actionsContainer?.nativeElement.appendChild(action.nativeElement);
    });
  }

  get cardHeadingClasses() {
    return {
      [this.elementRef.nativeElement.getAttribute('class') ?? '']: true,
      'border-b border-gray-200 px-4 py-5 sm:px-6': true
    };
  }
}
