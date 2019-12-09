import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { IncidentService } from '../services/incident.service';
import { Incident } from '../models/incident';
import { IncidentType } from '../models/incidenttype';
import { Observable } from 'rxjs';
import * as moment from 'moment';


@Component({
  selector: 'app-incident-add-edit',
  templateUrl: './incident-add-edit.component.html',
  styleUrls: ['./incident-add-edit.component.scss']
})
export class IncidentAddEditComponent implements OnInit {

  form: FormGroup;
  actionType: string;
  selectedIncident: number;
  incidentId: number;
  existingIncident: Incident;
  incidentTypes$: Observable<IncidentType[]>;

  constructor(private incidentService: IncidentService,private formBuilder: FormBuilder, 
    private activatedRoute : ActivatedRoute,
    private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    if (this.activatedRoute.snapshot.params[idParam]) {
      this.incidentId = this.activatedRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        incidentId: 0,
        description: ['', [Validators.required]],
        incidentDate: ['', [Validators.required]],
        incidentTime: ['', [Validators.required]],
        person: [''],
        incidentType: [0, [Validators.required]]
      }
    )
  }

  ngOnInit() {
    this.loadIncidentTypes();
    if (this.incidentId > 0) {
      this.actionType = 'Edit';
      this.incidentService.getIncident(this.incidentId)
        .subscribe(data => (
          this.existingIncident = data,
          this.form.controls['description'].setValue(data.description),
          this.form.controls['person'].setValue(data.person),
          this.form.controls['incidentDate'].setValue(data.incidentDate),
          this.form.controls['incidentTime'].setValue(data.incidentTime),
          this.form.controls['incidentType'].setValue(data.incidentTypeId)
        ));
    }
    
  }

  loadIncidentTypes(){
    this.incidentTypes$ = this.incidentService.getIncidentTypes();
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      let incident = this.getIncident(0);
      this.incidentService.saveIncident(incident)
        .subscribe((data) => {
          this.cancel();
        });
    }

    if (this.actionType === 'Edit') {       
      let incident = this.getIncident(this.existingIncident.id);
      this.incidentService.updateIncident(incident.id, incident)
        .subscribe((data) => {
          //this.router.navigate([this.router.url]);
          this.cancel();
        });
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }

  getIncident(incidentId){
    var localDate = new Date(this.form.get('incidentDate').value);
    let utcDate = moment(localDate).utc().toDate();
    var localTime = new Date(this.form.get('incidentTime').value);
    let utcTime = moment(localTime).utc().toDate();

    let incident: Incident = {
      id:incidentId,
      description:this.form.get('description').value,
      incidentDate:utcDate,
      incidentTime:utcTime,
      person:this.form.get('person').value,
      incidentTypeId: this.form.get('incidentType').value
     };
     return incident;
  }

  get description() { return this.form.get('description'); }
}
