import { Component } from '@angular/core';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { ActivatedRoute } from '@angular/router';
import { Race } from '../../race.interface';
import { RaceService } from '../../race.service';
import { MatCardModule } from '@angular/material/card';
import { CdkDropList, CdkDrag, CdkDragDrop } from '@angular/cdk/drag-drop';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-view-race',
  standalone: true,
  imports: [
    CdkAccordionModule,
    MatCardModule,
    CdkDropList,
    CdkDrag,
    MatDividerModule,
  ],
  templateUrl: './view-race.component.html',
  styleUrl: './view-race.component.scss',
})
export class ViewRaceComponent {
  race: Race | undefined;
  constructor(
    private route: ActivatedRoute,
    private raceService: RaceService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      const raceId = params.get('id');
      if (raceId) {
        this.raceService.getRaceById(raceId).subscribe({
          next: (race) => {
            this.race = race;
            this.race.traits.push({
              name: 'Size',
              description:
                'Dwarves stand between 4 and 5 feet tall and average about 150 pounds. Your size is Medium.',
            });
            this.race.traits.push({
              name: 'Creature Type',
              description: 'You are humanoid.',
            });
            this.race.traits.push({
              name: 'Movement Speed',
              description:
                'Your base walking speed is 25 feet. Your speed is not reduced by wearing heavy armor.',
            });
          },
          error: (error) => console.error(error),
        });
      }
    });
  }

  addTrait() {}

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
