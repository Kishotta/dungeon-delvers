import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {ContainerComponent} from "./common/components/container/container.component";
import {CardComponent} from "./common/components/cards/card/card.component";
import {CardBodyComponent} from "./common/components/cards/card-body/card-body.component";
import {CardHeaderComponent} from "./common/components/cards/card-header/card-header.component";
import {CardFooterComponent} from "./common/components/cards/card-footer/card-footer.component";
import {CardHeadingComponent} from "./common/components/cards/card-heading/card-heading.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ContainerComponent, CardComponent, CardBodyComponent, CardHeaderComponent, CardFooterComponent, CardHeadingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'dungeon-delvers';
}
