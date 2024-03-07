import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-trait-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './add-trait-dialog.component.html',
  styleUrl: './add-trait-dialog.component.scss',
})
export class AddTraitDialogComponent {
  data: { name: string; description: string } = { name: '', description: '' };

  constructor(public dialogRef: MatDialogRef<AddTraitDialogComponent>) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
