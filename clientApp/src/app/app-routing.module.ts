import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IncidentsComponent } from './incidents/incidents.component';
import { IncidentComponent } from './incident/incident.component';
import { IncidentAddEditComponent } from './incident-add-edit/incident-add-edit.component';


const routes: Routes = [
  { path: '', component: IncidentsComponent, pathMatch: 'full' },
  { path: 'incident/:id', component: IncidentComponent },
  { path: 'add', component: IncidentAddEditComponent },
  { path: 'incident/edit/:id', component: IncidentAddEditComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
