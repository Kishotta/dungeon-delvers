import {Component, ElementRef, Input} from '@angular/core';
import {NgClass} from "@angular/common";
import {BooleanInput, coerceBooleanProperty} from "../../../core/coercion/BooleanCoercion";

@Component({
  selector: 'container',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './container.component.html',
  styleUrl: './container.component.css'
})
export class ContainerComponent {
  @Input()
  get constrained(): boolean {
    return this._constrained;
  }
  set constrained(value: BooleanInput) {
    this._constrained = coerceBooleanProperty(value);
  }
  private _constrained: boolean = false;

  @Input()
  get fullWidthOnMobile(): boolean {
    return this._fullWidthOnMobile;
  }
  set fullWidthOnMobile(value: BooleanInput) {
    this._fullWidthOnMobile = coerceBooleanProperty(value);
  }
  _fullWidthOnMobile: boolean = false;

  @Input()
  get constrainedToBreakpoint(): boolean {
    return this._constrainedToBreakpoint;
  }
  set constrainedToBreakpoint(value: BooleanInput) {
    this._constrainedToBreakpoint = coerceBooleanProperty(value);
  }
  _constrainedToBreakpoint: boolean = false;

  constructor(private elementRef: ElementRef) {}

  get containerClasses() {
    return {
      [this.elementRef.nativeElement.getAttribute('class') ?? '']: true,
      'mx-auto': true,
      'max-w-7xl': this._constrained,
      'px-4': !this._fullWidthOnMobile,
      'sm:px-6': this._fullWidthOnMobile,
      'lg:px-8': this._fullWidthOnMobile,
      'container': this._constrainedToBreakpoint,
    };
  }
}
