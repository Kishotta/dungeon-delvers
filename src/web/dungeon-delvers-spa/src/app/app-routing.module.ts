import { NgModule, inject } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './core/landing-page/landing-page.component';
import { DashboardComponent } from './core/dashboard/dashboard.component';
import { AuthGuard } from './auth.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent, pathMatch: 'full' },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
