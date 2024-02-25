import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CardComponent } from './components/card/card.component';
import { DescriptionListItemComponent } from './components/description-list-item/description-list-item.component';

@NgModule({
  declarations: [CardComponent, DescriptionListItemComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    CardComponent,
    DescriptionListItemComponent,
  ],
})
export class SharedModule {}
