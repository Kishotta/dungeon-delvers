import { Component } from '@angular/core';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Race } from '../../race.interface';
import { RaceService } from '../../race.service';
import { MatCardModule } from '@angular/material/card';
import { CdkDropList, CdkDrag, CdkDragDrop } from '@angular/cdk/drag-drop';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialog } from '@angular/material/dialog';
import { AddTraitDialogComponent } from '../add-trait-dialog/add-trait-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-view-race',
  standalone: true,
  imports: [
    RouterModule,
    CdkAccordionModule,
    MatCardModule,
    CdkDropList,
    CdkDrag,
    MatDividerModule,
    MatSnackBarModule,
    MatButtonModule,
  ],
  templateUrl: './view-race.component.html',
  styleUrl: './view-race.component.scss',
})
export class ViewRaceComponent {
  race!: Race;
  constructor(
    private route: ActivatedRoute,
    private raceService: RaceService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadRace();
  }

  loadRace() {
    this.route.paramMap.subscribe((params) => {
      const raceId = params.get('id');
      if (raceId) {
        this.raceService.getRaceById(raceId).subscribe({
          next: (race) => {
            this.race = race;
          },
          error: (error) => console.error(error),
        });
      }
    });
  }

  openAddTraitDialog(): void {
    const dialogRef = this.dialog.open(AddTraitDialogComponent, {
      data: { name: '', description: '' },
      width: '600px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log(result);
        if (result.name && result.description) {
          this.raceService
            .addTrait(this.race, result.name, result.description)
            ?.subscribe({
              next: (trait) => {
                this.showSnackBar('Trait added successfully');
                this.loadRace();
              },
              error: (error) => {
                this.showSnackBar('Failed to add trait');
              },
            });
        }
      }
    });
  }

  showSnackBar(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 2000,
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      //   moveItemInArray(
      //     event.container.data,
      //     event.previousIndex,
      //     event.currentIndex
      //   );
      // } else {
      //   transferArrayItem(
      //     event.previousContainer.data,
      //     event.container.data,
      //     event.previousIndex,
      //     event.currentIndex
      //   );
    }
  }
}
