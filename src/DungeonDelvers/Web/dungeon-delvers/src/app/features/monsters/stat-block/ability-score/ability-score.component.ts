import {Component, Input} from '@angular/core';
import {CardBodyComponent} from "../../../../common/components/cards/card-body/card-body.component";
import {CardComponent} from "../../../../common/components/cards/card/card.component";
import {NgClass} from "@angular/common";

@Component({
  selector: 'ability-score',
  standalone: true,
  imports: [
    CardBodyComponent,
    CardComponent,
    NgClass
  ],
  templateUrl: './ability-score.component.html',
  styleUrl: './ability-score.component.css'
})
export class AbilityScoreComponent {
  @Input()
  stat: string = 'STR';

  @Input()
  score: number = 10;

  get modifier(): number {
    return Math.floor((this.score - 10) / 2) ;
  }

  get modifierString(): string {
    return this.modifier >= 0 ? `+${this.modifier}` : `${this.modifier}`;
  }
}
