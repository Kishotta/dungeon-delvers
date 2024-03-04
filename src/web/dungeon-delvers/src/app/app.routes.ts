import { Routes } from '@angular/router';
import { DashboardComponent } from './core/components/dashboard/dashboard.component';
import { authGuard } from './core/auth.guard';
import { RacesComponent } from './modules/races/components/races/races.component';
import { ViewRaceComponent } from './modules/races/components/view-race/view-race.component';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'races', component: RacesComponent, canActivate: [authGuard] },
  { path: 'races/:id', component: ViewRaceComponent, canActivate: [authGuard] },
];
