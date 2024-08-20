import {Component, ElementRef, Input} from '@angular/core';
import {NgClass} from "@angular/common";
import {BooleanInput, coerceBooleanProperty} from "../../../../core/coercion/BooleanCoercion";

@Component({
  selector: 'card',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './card.component.html',
  styleUrl: './card.component.css'
})
export class CardComponent {
  @Input()
  get edgeToEdgeOnMobile(): boolean {
    return this._edgeToEdgeOnMobile;
  }
  set edgeToEdgeOnMobile(value: BooleanInput) {
    this._edgeToEdgeOnMobile = coerceBooleanProperty(value);
  }
  _edgeToEdgeOnMobile: boolean = false;

  @Input()
  get allowOverflow(): boolean {
    return this._allowOverflow;
  }
  set allowOverflow(value: BooleanInput) {
    this._allowOverflow = coerceBooleanProperty(value);
  }
  _allowOverflow: boolean = false;

  @Input()
  get divided(): boolean {
    return this._divided;
  }
  set divided(value: BooleanInput) {
    this._divided = coerceBooleanProperty(value);
  }
  _divided: boolean = false;

  @Input()
  get well(): boolean {
    return this._well;
  }
  set well(value: BooleanInput) {
    this._well = coerceBooleanProperty(value);
  }
  _well: boolean = false;

  @Input()
  get wellOnGray(): boolean {
    return this._wellOnGray;
  }
  set wellOnGray(value: BooleanInput) {
    this._wellOnGray = coerceBooleanProperty(value);
  }
  _wellOnGray: boolean = false;

  constructor(private elementRef: ElementRef) {}

  get cardClasses() {
    return {
      [this.elementRef.nativeElement.getAttribute('class') ?? '']: true,
      'overflow-hidden': !this._allowOverflow,
      'rounded-lg': !this._edgeToEdgeOnMobile,
      'sm:rounded-lg': this._edgeToEdgeOnMobile,
      'divide-y divide-solid divide-gray-200': this._divided,
      'bg-white shadow': !this._well && !this._wellOnGray,
      'bg-gray-50': this._well,
      'bg-gray-200': this._wellOnGray
    }
  }
}
