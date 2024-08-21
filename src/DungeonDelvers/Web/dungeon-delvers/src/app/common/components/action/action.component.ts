import {Attribute, Component, ElementRef, Input} from '@angular/core';
import {NgClass} from "@angular/common";
import {BooleanInput, coerceBooleanProperty} from "../../../core/coercion/BooleanCoercion";
import {coerceActionSizeProperty} from "./action.module";

export enum ActionSize {
  ExtraSmall = 'extra-small',
  Small = 'small',
  Medium = 'medium',
  Large = 'large',
  ExtraLarge = 'extra-large',
}

@Component({
  selector: 'action',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './action.component.html',
  styleUrl: './action.component.css'
})
export class ActionComponent {
  @Input()
  get size(): ActionSize {
    return this._size;
  }
  set size(value: string) {
    this._size = coerceActionSizeProperty(value);
  }
  private _size: ActionSize = ActionSize.Medium;

  @Input()
  get secondary(): boolean {
    return this._secondary;
  }
  set secondary(value: BooleanInput) {
    this._secondary = coerceBooleanProperty(value);
  }
  private _secondary: boolean = false

  @Input()
  get rounded(): boolean {
    return this._rounded;
  }
  set rounded(value: BooleanInput) {
    this._rounded = coerceBooleanProperty(value);
  }
  private _rounded: boolean = false;

  @Input()
  get circular(): boolean {
    return this._circular;
  }
  set circular(value: BooleanInput) {
    this._circular = coerceBooleanProperty(value);
  }
  private _circular: boolean = false;

  constructor(
    private elementRef: ElementRef,
    @Attribute('type') protected type: string = 'button',
    @Attribute('class') private classes: string
  ) {}

  get actionClasses() {
    return {
      [this.classes ?? '']: true,
      'inline-flex items-center font-semibold': true,
      'text-xs shadow-sm': this._size === ActionSize.ExtraSmall,
      'text-sm shadow-sm': this._size !== ActionSize.ExtraSmall,
      'focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2': !this._secondary,
      'bg-white text-gray-900 hover:bg-gray-50': this._secondary,
      'rounded-md': !this._rounded && !this._circular,
      'rounded-full': this._rounded || this._circular,
      'px-2 py-1 gap-x-1': this._size === ActionSize.ExtraSmall && !this._circular,
      'px-2 py-1 gap-x-1.5': this._size === ActionSize.Small && !this._circular,
      'p-1': (this._size === ActionSize.ExtraSmall || this._size === ActionSize.Small) && this._circular,
      'px-2.5 py-1.5 gap-x-1.5': this._size === ActionSize.Medium && !this._circular,
      'p-1.5': this._size === ActionSize.Medium && this._circular,
      'px-3 py-2 gap-x-1.5': this._size === ActionSize.Large && !this._circular,
      'px-3.5 py-2.5 gap-x-2': this._size === ActionSize.ExtraLarge && !this._circular,
      'p-2': (this._size === ActionSize.Large || this._size === ActionSize.ExtraLarge) && this._circular,
    }
  }

  getElementRef() {
    return this.elementRef;
  }
}
