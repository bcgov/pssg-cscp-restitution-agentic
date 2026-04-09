import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { healthCheckGuard } from './guards/health-check.guard';
import { NotFoundComponent } from './not-found/not-found.component';
import { OutageComponent } from './outage/outage.component';
import { RestitutionApplicationComponent } from './restitution-application/restitution-application.component';
import { ResitutionForm } from './shared/enums-list';
import { RestitutionSuccessComponent } from './shared/restitution/success/restitution-success.component';

const routes: Routes = [
  {
    path: 'outage',
    component: OutageComponent
  },
  {
    path: '',
    redirectTo: '/victim',
    pathMatch: 'full'
  },
  {
    path: 'victim',
    component: RestitutionApplicationComponent,
    canActivate: [healthCheckGuard],
    data: { formType: ResitutionForm.Victim }
  },
  {
    path: 'offender',
    component: RestitutionApplicationComponent,
    canActivate: [healthCheckGuard],
    data: { formType: ResitutionForm.Offender }
  },
  {
    path: 'victim-entity',
    component: RestitutionApplicationComponent,
    canActivate: [healthCheckGuard],
    data: { formType: ResitutionForm.VictimEntity }
  },
  {
    path: 'restitution-success',
    component: RestitutionSuccessComponent,
    canActivate: [healthCheckGuard]
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule {}
