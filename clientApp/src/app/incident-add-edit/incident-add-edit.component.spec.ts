import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncidentAddEditComponent } from './incident-add-edit.component';

describe('IncidentAddEditComponent', () => {
  let component: IncidentAddEditComponent;
  let fixture: ComponentFixture<IncidentAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncidentAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncidentAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
