import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IncidentService } from '../services/incident.service';
import { Incident } from '../models/incident';

@Component({
  selector: 'app-incident',
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.scss']
})
export class IncidentComponent implements OnInit {
  incident$: Observable<Incident>;
  incidentId: number;

  constructor(private incidentService: IncidentService, private activatedRoute : ActivatedRoute, private router: Router) {
    const idParam = 'id';
    if (this.activatedRoute.snapshot.params[idParam]) {
      this.incidentId = this.activatedRoute.snapshot.params[idParam];
    }    
   }

  ngOnInit() {
    this.loadIncident();
  }

  loadIncident(){
    this.incident$ = this.incidentService.getIncident(this.incidentId);
  }

  cancel() {
    this.router.navigate(['/']);
  }
}
