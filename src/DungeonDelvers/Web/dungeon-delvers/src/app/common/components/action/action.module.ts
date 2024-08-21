import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ActionComponent, ActionSize} from "./action.component";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ActionComponent
  ],
  exports: [
    ActionComponent
  ]
})
export class ActionModule { }

export function coerceActionSizeProperty(value: string): ActionSize {
  return value as ActionSize;
}
