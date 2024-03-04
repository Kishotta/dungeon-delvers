import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-create-race-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './create-race-dialog.component.html',
  styleUrl: './create-race-dialog.component.scss',
})
export class CreateRaceDialogComponent {
  data: { name: string } = { name: '' };

  constructor(public dialogref: MatDialogRef<CreateRaceDialogComponent>) {}

  onNoClick(): void {
    this.dialogref.close();
  }
}
