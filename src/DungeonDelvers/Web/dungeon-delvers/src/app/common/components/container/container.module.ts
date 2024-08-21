import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ContainerComponent} from "./container.component";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ContainerComponent
  ],
  exports: [
    ContainerComponent
  ]
})
export class ContainerModule { }
