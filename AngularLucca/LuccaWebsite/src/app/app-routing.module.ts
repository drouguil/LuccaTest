// generic Angular

import { NgModule } from '@angular/core';

// Routing

import { RouterModule, Routes } from '@angular/router';

// Pages

import { HomeComponent, NotFoundComponent, DestinationComponent } from './pages';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'destination/:id', component: DestinationComponent },
  { path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
