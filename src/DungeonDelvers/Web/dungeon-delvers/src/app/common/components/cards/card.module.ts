import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CardComponent} from "./card/card.component";
import {CardHeaderComponent} from "./card-header/card-header.component";
import {CardHeadingComponent} from "./card-heading/card-heading.component";
import {CardBodyComponent} from "./card-body/card-body.component";
import {CardFooterComponent} from "./card-footer/card-footer.component";

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CardComponent,
    CardHeaderComponent,
    CardHeadingComponent,
    CardBodyComponent,
    CardFooterComponent
  ],
  exports: [
    CardComponent,
    CardHeaderComponent,
    CardHeadingComponent,
    CardBodyComponent,
    CardFooterComponent
  ]
})
export class CardModule { }
