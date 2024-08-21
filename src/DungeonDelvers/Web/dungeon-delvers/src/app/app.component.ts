import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {ActionModule} from "./common/components/action/action.module";
import {CardModule} from "./common/components/cards/card.module";
import {ContainerModule} from "./common/components/container/container.module";
import {AbilityScoreComponent} from "./features/monsters/stat-block/ability-score/ability-score.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    ActionModule,
    ContainerModule,
    CardModule,
    AbilityScoreComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'dungeon-delvers';
}
