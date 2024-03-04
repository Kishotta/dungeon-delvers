import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableModule, MatTable } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { RacesDataSource } from './races-datasource';
import { RaceService } from '../../race.service';
import { Race } from '../../race.interface';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { CreateRaceDialogComponent } from '../create-race-dialog/create-race-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-races',
  templateUrl: './races.component.html',
  styleUrl: './races.component.scss',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCardModule,
    MatMenuModule,
    MatSnackBarModule,
    MatButtonModule,
    MatIconModule,
  ],
})
export class RacesComponent implements AfterViewInit {
  @ViewChild(MatTable) table!: MatTable<Race>;
  dataSource!: RacesDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'traits', 'actions'];

  constructor(
    public dialog: MatDialog,
    private raceService: RaceService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.dataSource = new RacesDataSource(this.raceService);
    this.dataSource.loadRaces();
  }

  ngAfterViewInit(): void {
    this.table.dataSource = this.dataSource;
  }

  openCreateRaceDialog(): void {
    const dialogRef = this.dialog.open(CreateRaceDialogComponent, {
      data: { name: '' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log(result);
        this.raceService.createRace(result.name).subscribe({
          next: (race) => {
            this.showSnackBar('Race created successfully');
            this.dataSource.loadRaces(); // Method to re-fetch the races
          },
          error: (error) => {
            this.showSnackBar('Failed to create race');
          },
        });
      }
    });
  }

  viewDetails(race: Race): void {
    this.router.navigate(['/races', race.id]);
  }

  showSnackBar(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 2000,
    });
  }
}
