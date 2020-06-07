import { Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { CaixaPromessasComponent } from './caixa-promessas/caixa-promessas.component';
import { AboutComponent } from './about/about.component';

export const ROUTES: Routes = [
  { path: 'biblia-app/', component: HomeComponent },
  { path: 'biblia-app/home', component: HomeComponent },
  { path: 'biblia-app/about', component: AboutComponent },
  { path: 'biblia-app/caixaPromessas', component: CaixaPromessasComponent }
];
