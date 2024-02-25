import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainpageComponent } from './layout/mainpage/mainpage.component';

const routes: Routes = [
  {
    path: '',
    component: MainpageComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./modules/home/home.module').then((m) => m.HomeModule),
      },
      {
        path: 'about',
        loadChildren: () =>
          import('./modules/about/about.module').then((m) => m.AboutModule),
      },
      {
        path: 'contact',
        loadChildren: () =>
          import('./modules/contact/contact.module').then(
            (m) => m.ContactModule
          ),
      },
      {
        path: 'characters',
        loadChildren: () =>
          import('./modules/characters/characters.module').then(
            (m) => m.CharactersModule
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
